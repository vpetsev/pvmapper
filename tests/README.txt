Run portable regressions with Node.js 18 or later:
  node --test tests/regressions.test.cjs

The scoring tests execute both editions' committed JavaScript. Proxy policy
checks cover the deployed allowlist configuration and redirect guard, but are
not a substitute for .NET Framework/IIS integration testing on Windows.

Before deploying the proxy, verify the allowlisted map-service URLs still work.
Redirects are deliberately not followed on the server, so a service that has
moved must have its final approved URL configured explicitly.
