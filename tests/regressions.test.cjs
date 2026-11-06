const test = require('node:test');
const assert = require('node:assert/strict');
const fs = require('node:fs');
const path = require('node:path');
const vm = require('node:vm');

for (const edition of ['Doe.PVMapper', 'Doe.PVMapper-Dev']) {
  const root = path.join(__dirname, '..', 'pvmapper', 'sourceCode\\sourceCode', edition, 'Doe.PVMapper');
  test(`${edition}: increasing and decreasing scoring boundaries stay continuous`, () => {
    const context = {pvMapper:{ScoreUtilityWindows:{basicWindow:{setup(){},okhandler(){}}}}};
    vm.createContext(context);
    vm.runInContext(fs.readFileSync(path.join(root,'Scripts/pvMapper/TSMapper/UtilityFunctions.js'),'utf8'),context);
    const score = context.pvMapper.UtilityFunctions.sinusoidal.fn;
    const ascending = {minValue:0,maxValue:100,target:50,slope:1};
    const descending = {...ascending,minValue:100,maxValue:0};
    for (const x of [-10,0]) { assert.equal(score(x,ascending),0); assert.equal(score(x,descending),1); }
    for (const x of [100,110]) { assert.equal(score(x,ascending),1); assert.equal(score(x,descending),0); }
    assert.equal(score(50,ascending),0.5);
    assert.equal(score(50,descending),0.5);
    assert(Math.abs(score(0,descending)-score(0.01,descending))<0.001);
    assert(Math.abs(score(100,descending)-score(99.99,descending))<0.001);
  });
  test(`${edition}: deployed proxy policy requires an allowlisted destination`, () => {
    const config = fs.readFileSync(path.join(root,'Proxy/proxy.config'),'utf8').replace(/<!--[\s\S]*?-->/g,'');
    assert.match(config, /<ProxyConfig[^>]*mustMatch="true"/);
    assert.match(config, /<serverUrl\s+url="https?:\/\//);
    const handler = fs.readFileSync(path.join(root,'Proxy/proxy.ashx'),'utf8');
    assert.match(handler, /req\.AllowAutoRedirect\s*=\s*false/);
    assert.match(handler, /Uri\.TryCreate\(uri, UriKind\.Absolute/);
  });
}
