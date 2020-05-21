Module Geo_Function

    Public Const PI As Double = 3.14159265

    Public Structure POINTAPI
        Dim X As Double 'Long
        Dim Y As Double 'Long
        Dim Z As Double ' 
    End Structure

    Public Structure LineType
        Dim Pt1 As POINTAPI
        Dim Pt2 As POINTAPI
        Dim Dx As Double
        Dim DY As Double
    End Structure

    Public Structure CircleType
        Dim pt As POINTAPI
        Dim R As Double
    End Structure

    Public Structure PathType
        Dim nPt As Integer
        Dim pt() As POINTAPI
    End Structure

    Public Structure grdGENESIS
        'Dim Lt As LineType
        Dim BasePt As POINTAPI
        Dim ShorePt As POINTAPI
        Dim Distance As Double ' 
    End Structure

    Enum PerpendArm
        Left = 1
        Right = 2
    End Enum

    Public LL As LineType
    Public BcPath As PathType
    Public BcPathOffset As PathType

    '{ID:GEO-001}
    Function LineGIS(ByVal X1 As Double, ByVal Y1 As Double, ByVal x2 As Double, ByVal y2 As Double, ByVal Lwidth As Long, ByVal LColor As System.Drawing.Color) As Boolean
        Dim hDraw As Integer
        hDraw = g_MW.View.Draw.NewDrawing(1)
        g_MW.View.Draw.DrawLine(X1, Y1, x2, y2, Lwidth, LColor)
        LineGIS = True
    End Function

    '{ID:GEO-002}
    Function PointGIS(ByVal X1 As Double, ByVal Y1 As Double, ByVal Pwidth As Long, ByVal PColor As System.Drawing.Color) As Boolean
        Dim hDraw As Integer
        hDraw = g_MW.View.Draw.NewDrawing(1)
        g_MW.View.Draw.DrawPoint(X1, Y1, Pwidth, PColor)
        PointGIS = True
    End Function

    '{ID:GEO-002A}
    Function CrossGIS(ByVal X1 As Double, ByVal Y1 As Double, ByVal L As Double, ByVal Pwidth As Long, ByVal PColor As System.Drawing.Color) As Boolean
        Dim hDraw As Integer
        hDraw = g_MW.View.Draw.NewDrawing(1)
        g_MW.View.Draw.DrawLine(X1 - L, Y1, X1 + L, Y1, Pwidth, PColor)
        g_MW.View.Draw.DrawLine(X1, Y1 - L, X1, Y1 + L, Pwidth, PColor)
        CrossGIS = True
    End Function

    '{ID:GEO-003}
    Public Sub Centroid(ByVal BoundaryPath As PathType, ByRef CentroidPoint As POINTAPI)
        Dim pt As Integer
        Dim second_factor As Double
        Dim polygon_area As Double

        'Add the first point at the end of the array.
        ReDim Preserve BoundaryPath.pt(BoundaryPath.nPt + 1)
        BoundaryPath.pt(BoundaryPath.nPt + 1) = BoundaryPath.pt(1)
        Dim X, Y As Double
        ' Find the centroid.
        X = 0
        Y = 0
        For pt = 1 To BoundaryPath.nPt - 1 ' m_NumPoints
            second_factor = _
                BoundaryPath.pt(pt).X * BoundaryPath.pt(pt + 1).Y - _
                BoundaryPath.pt(pt + 1).X * BoundaryPath.pt(pt).Y
            X = X + (BoundaryPath.pt(pt).X + BoundaryPath.pt(pt + 1).X) * second_factor
            Y = Y + (BoundaryPath.pt(pt).Y + BoundaryPath.pt(pt + 1).Y) * second_factor
        Next pt

        ' Divide by 6 times the polygon's area.
        polygon_area = PathArea(BoundaryPath)               '{GEO-004}
        X = X / 6 / polygon_area
        Y = Y / 6 / polygon_area

        ' If the values are negative, the polygon is
        ' oriented counterclockwise. Reverse the signs.
        If X < 0 Then
            X = -X
            Y = -Y
        End If
        CentroidPoint.X = X
        CentroidPoint.Y = Y
        '    frmMap.MAP.CurrentX = X
        '    frmMap.MAP.CurrentY = Y
        '    frmMap.MAP.Print "Centroid of Planted Area"
    End Sub

    '{ID:GEO-004}
    Function PathArea(ByVal mPath As PathType) As Double
        Dim area As Double
        Dim pt As Integer
        area = 0
        mPath.pt(mPath.nPt + 1) = mPath.pt(1)
        For pt = 1 To mPath.nPt
            area = area + ((mPath.pt(pt).X * mPath.pt(pt + 1).Y) - (mPath.pt(pt + 1).X * mPath.pt(pt).Y)) / 2
        Next pt
        If area < 0 Then area = Math.Abs(area)
        PathArea = area
    End Function

    '{ID:GEO-005}
    Function Length(ByVal mLine As LineType) As Double
        Length = (diffX(mLine) ^ 2 + diffY(mLine) ^ 2) ^ 0.5 '{GEO-009} {GEO-010}
    End Function

    '{ID:GEO-006}
    Function PathLength(ByVal Pth As PathType) As Double
        Dim LP As LineType
        Dim I As Integer
        PathLength = 0
        For I = 1 To Pth.nPt - 1
            LP.Pt1.X = Pth.pt(I).X
            LP.Pt1.Y = Pth.pt(I).Y
            LP.Pt2.X = Pth.pt(I + 1).X
            LP.Pt2.Y = Pth.pt(I + 1).Y
            PathLength = PathLength + Length(LP) '{GEO-005}
        Next
    End Function

    '{ID:GEO-007}
    Function PointInPath(ByVal Pth As PathType, ByVal Dist As Double, ByVal ID As Integer) As POINTAPI
        Dim LP As LineType
        Dim PL1, PL2 As Double
        If Dist = 0 Or Dist = PathLength(Pth) Then  '{GEO-006}
            PointInPath = Pth.pt(1)
            Exit Function
        End If
        If Dist > 0 And Dist < PathLength(Pth) Then  '{GEO-006}
            PL1 = 0
            PL2 = 0
            Dim I As Integer
            For I = 1 To Pth.nPt - 1
                LP.Pt1.X = Pth.pt(I).X
                LP.Pt1.Y = Pth.pt(I).Y
                LP.Pt2.X = Pth.pt(I + 1).X
                LP.Pt2.Y = Pth.pt(I + 1).Y
                PL2 = PL2 + Length(LP)                    '{GEO-005}
                If PL1 <= Dist And Dist < PL2 Then
                    Dim subDist As Double
                    subDist = Dist - PL1
                    PointInPath = PointInLine(LP, subDist)  '{GEO-008}
                    ID = I
                    Exit Function
                End If
                PL1 = PL2
            Next
        Else
            'MsgBox "Function Error"
        End If
    End Function

    '{ID:GEO-008}
    Function PointInLine(ByVal L As LineType, ByVal subDist As Double) As POINTAPI
        PointInLine.X = subDist / Length(L) * (L.Pt2.X - L.Pt1.X) + L.Pt1.X '{GEO-005}
        PointInLine.Y = subDist / Length(L) * (L.Pt2.Y - L.Pt1.Y) + L.Pt1.Y '{GEO-005}
    End Function

    '{ID:GEO-009}
    Function diffX(ByVal mLine As LineType) As Double
        diffX = mLine.Pt2.X - mLine.Pt1.X
    End Function

    '{ID:GEO-010}
    Function diffY(ByVal mLine As LineType) As Double
        diffY = mLine.Pt2.Y - mLine.Pt1.Y
    End Function

    '{ID:GEO-011}
    Function AzmAngle(ByVal mLine As LineType) As Double
        If diffX(mLine) = 0 Or diffY(mLine) = 0 Then      '{GEO-009} {GEO-010}
            'Vertical Case
            If diffX(mLine) = 0 Then                        '{GEO-009}
                If mLine.Pt1.Y > mLine.Pt2.Y Then AzmAngle = 0
                If mLine.Pt1.Y < mLine.Pt2.Y Then AzmAngle = 180
            End If
            'Horizontal Case
            If diffY(mLine) = 0 Then                        '{GEO-010}
                If mLine.Pt1.X > mLine.Pt2.X Then AzmAngle = 270
                If mLine.Pt1.X < mLine.Pt2.X Then AzmAngle = 90
            End If
        Else
            'Angle Computation
            Dim angR As Double
            angR = Math.Atan(diffY(mLine) / diffX(mLine))         '{GEO-010}   {GEO-009}
            If mLine.Pt1.Y < mLine.Pt2.Y Then
                If mLine.Pt1.X < mLine.Pt2.X Then
                    'Quadrant 1
                    AzmAngle = 90 - angR * 180 / PI
                Else
                    'Qurdrant 2
                    AzmAngle = 270 - angR * 180 / PI
                End If
            Else
                If mLine.Pt1.X < mLine.Pt2.X Then
                    'Qurdrant 3
                    AzmAngle = 90 - angR * 180 / PI
                Else
                    'Qurdrant 4
                    AzmAngle = 270 - angR * 180 / PI
                End If
            End If
        End If
    End Function

    '{ID:GEO-012}
    'Sub spiltRegion(ByVal ID As Integer, ByVal d1, ByVal d2, ByVal Pth As PathType)  '(ID, id1, P1 As POINTAPI, id2, P2 As POINTAPI)
    '    Dim desx, desy
    '    Dim bc As LineType
    '    Dim I, j, ii
    '    Dim LineSplit(2), ptSplit(2) As POINTAPI
    '    ii = 0
    '    Dim dTemp
    '    If d1 > d2 Then
    '        dTemp = d1
    '        d1 = d2
    '        d2 = dTemp
    '    End If

    '    'Dim LineSplit(2), ptSplit(2) As POINTAPI
    '    'Dim i, j
    '    ptSplit(0) = DistToPoint(Pth, d1)                     '{GEo-030}
    '    ptSplit(1) = DistToPoint(Pth, d2)                     '{GEo-030}
    '    Dim L As LineType
    '    Dim sumL, oSumL
    '    sumL = 0
    '    oSumL = sumL
    '    For I = 1 To Pth.nPt - 1
    '        L.Pt1 = Pth.pt(I)
    '        L.Pt2 = Pth.pt(I + 1)
    '        sumL = sumL + Geometry.Length(L)
    '        If oSumL <= d1 And d1 < sumL Then LineSplit(0) = I
    '        If oSumL <= d2 And d2 < sumL Then LineSplit(1) = I
    '        oSumL = sumL
    '    Next

    '    '  Dim VertexA() As POINTAPI
    '    '  Dim nSubVertexA As Integer
    '    '  Dim VertexB() As POINTAPI
    '    '  Dim nSubVertexB As Integer

    '    'frmEvalutionTable.HindenGrd.TextMatrix(id, 1) = id1
    '    'frmEvalutionTable.HindenGrd.TextMatrix(id, 2) = id2

    '    frmEvalutionTable.HindenGrd.TextMatrix(ID, 3) = Format(ptSplit(0).X, "0.00")
    '    frmEvalutionTable.HindenGrd.TextMatrix(ID, 4) = Format(ptSplit(0).Y, "0.00")
    '    frmEvalutionTable.HindenGrd.TextMatrix(ID, 5) = Format(ptSplit(1).X, "0.00")
    '    frmEvalutionTable.HindenGrd.TextMatrix(ID, 6) = Format(ptSplit(1).Y, "0.00")

    '    '  Dim VertexA(20) As POINTAPI
    '    '  Dim nSubVertexA As Integer
    '    '  Dim VertexB(20) As POINTAPI
    '    '  Dim nSubVertexB As Integer

    '    '
    '    ' First Sub planted Area
    '    '
    '    nSubVertexA = 0
    '    ReDim VertexA(nSubVertexA)
    '    'If ii = 2 Then
    '    frmEvalutionTable.HindenGrd.TextMatrix(ID, 3) = Format(ptSplit(0).X, "0.00")
    '    frmEvalutionTable.HindenGrd.TextMatrix(ID, 4) = Format(ptSplit(0).Y, "0.00")
    '    frmEvalutionTable.HindenGrd.TextMatrix(ID, 5) = Format(ptSplit(1).X, "0.00")
    '    frmEvalutionTable.HindenGrd.TextMatrix(ID, 6) = Format(ptSplit(1).Y, "0.00")
    '    For I = 1 To nVertex - 1
    '        If I = LineSplit(0) Then
    '            'MAP.Line (BcPath.Pt(i).X, BcPath.Pt(i).Y)-(ptSplit(0).X, ptSplit(0).Y), vbRed
    '            nSubVertexA = nSubVertexA + 1
    '            ReDim Preserve VertexA(nSubVertexA)
    '            VertexA(nSubVertexA).X = BcPath.pt(I).X
    '            VertexA(nSubVertexA).Y = BcPath.pt(I).Y
    '            'MAP.Line (ptSplit(0).X, ptSplit(0).Y)-(ptSplit(1).X, ptSplit(1).Y), vbRed
    '            nSubVertexA = nSubVertexA + 1
    '            ReDim Preserve VertexA(nSubVertexA)
    '            VertexA(nSubVertexA).X = ptSplit(0).X
    '            VertexA(nSubVertexA).Y = ptSplit(0).Y
    '            I = LineSplit(1)
    '            'MAP.Line (ptSplit(1).X, ptSplit(1).Y)-(BcPath.Pt(i + 1).X, BcPath.Pt(i + 1).Y), vbRed
    '            nSubVertexA = nSubVertexA + 1
    '            ReDim Preserve VertexA(nSubVertexA)
    '            VertexA(nSubVertexA).X = ptSplit(1).X
    '            VertexA(nSubVertexA).Y = ptSplit(1).Y
    '        Else
    '            'MAP.Line (BcPath.Pt(i).X, BcPath.Pt(i).Y)-(BcPath.Pt(i + 1).X, BcPath.Pt(i + 1).Y), vbRed
    '            nSubVertexA = nSubVertexA + 1
    '            ReDim Preserve VertexA(nSubVertexA)
    '            VertexA(nSubVertexA).X = BcPath.pt(I).X
    '            VertexA(nSubVertexA).Y = BcPath.pt(I).Y
    '        End If
    '    Next
    '    nSubVertexA = nSubVertexA + 1
    '    ReDim Preserve VertexA(nSubVertexA)
    '    VertexA(nSubVertexA).X = VertexA(1).X 'BcPath.Pt(i).X
    '    VertexA(nSubVertexA).Y = VertexA(1).Y 'BcPath.Pt(i).Y
    '    '
    '    ' Create Sub Region
    '    '
    '    RegionA = CreatePolygonRgn(VertexA(1), nSubVertexA, 1)                            '{API-001}

    '    'frmMap.Map.DrawWidth = 3

    '    ' print node name
    '    '    For I = 1 To nSubVertexA - 1
    '    '      LineGIS VertexA(I).X, VertexA(I).Y, VertexA(I + 1).X, VertexA(I + 1).Y, 5, vbYellow
    '    '    Next
    '    Area1 = PolygonArea(nSubVertexA - 1, VertexA)                                                '{GEO-013}
    '    frmEvalutionTable.grdGA4Mainline.TextMatrix(ID, 1) = Format(Area1, "0.00")
    '    E(1) = Format(Area1, "0.00")
    '    '    frmMap.Map.DrawWidth = 1
    '    '
    '    ' Second Sub planted Area
    '    '
    '    nSubVertexB = 0
    '    ReDim VertexB(nSubVertexB)
    '    For I = 1 To nVertex - 1
    '        If I = LineSplit(0) Then
    '            'MAP.Line (ptSplit(0).X, ptSplit(0).Y)-(BcPath.Pt(i + 1).X, BcPath.Pt(i + 1).Y), vbYellow
    '            nSubVertexB = nSubVertexB + 1
    '            ReDim Preserve VertexB(nSubVertexB)
    '            VertexB(nSubVertexB).X = ptSplit(0).X
    '            VertexB(nSubVertexB).Y = ptSplit(0).Y
    '            For j = I + 1 To LineSplit(1) - 1
    '                'MAP.Line (BcPath.Pt(j).X, BcPath.Pt(j).Y)-(BcPath.Pt(j + 1).X, BcPath.Pt(j + 1).Y), vbYellow
    '                nSubVertexB = nSubVertexB + 1
    '                ReDim Preserve VertexB(nSubVertexB)
    '                VertexB(nSubVertexB).X = BcPath.pt(j).X
    '                VertexB(nSubVertexB).Y = BcPath.pt(j).Y
    '            Next
    '            'MAP.Line (BcPath.Pt(j).X, BcPath.Pt(j).Y)-(ptSplit(1).X, ptSplit(1).Y), vbYellow
    '            nSubVertexB = nSubVertexB + 1
    '            ReDim Preserve VertexB(nSubVertexB)
    '            VertexB(nSubVertexB).X = BcPath.pt(j).X
    '            VertexB(nSubVertexB).Y = BcPath.pt(j).Y
    '            'MAP.Line (ptSplit(1).X, ptSplit(1).Y)-(ptSplit(0).X, ptSplit(0).Y), vbYellow
    '            nSubVertexB = nSubVertexB + 1
    '            ReDim Preserve VertexB(nSubVertexB)
    '            VertexB(nSubVertexB).X = ptSplit(1).X
    '            VertexB(nSubVertexB).Y = ptSplit(1).Y
    '        End If
    '    Next
    '    nSubVertexB = nSubVertexB + 1
    '    ReDim Preserve VertexB(nSubVertexB)
    '    VertexB(nSubVertexB).X = VertexB(1).X 'BcPath.Pt(0).X
    '    VertexB(nSubVertexB).Y = VertexB(1).Y 'BcPath.Pt(0).Y
    '    '
    '    ' Create sub region
    '    '
    '    RegionB = CreatePolygonRgn(VertexB(1), nSubVertexB, 1)                       '{API-001}
    '    '
    '    ' Draw Sub region
    '    '
    '    '    For I = 1 To nSubVertexB - 1
    '    '      LineGIS VertexB(I).X, VertexB(I).Y, VertexB(I + 1).X, VertexB(I + 1).Y, 5, vbBlue
    '    '    Next

    '    Area2 = PolygonArea(nSubVertexB - 1, VertexB)                                      '{GEO-013}
    '    'Form1.Caption = Area2
    '    frmEvalutionTable.grdGA4Mainline.TextMatrix(ID, 2) = Format(Area2, "0.00")
    '    E(2) = Format(Area2, "0.00")
    '    '    frmMap.Map.DrawWidth = 1
    '    'Else
    '    'End If
    'End Sub

    '{ID:GEO-013}
    Function PolygonArea(ByVal nPoints As Integer, ByVal R_Point() As POINTAPI) As Double
        Dim area
        Dim pt
        area = 0
        R_Point(nPoints + 1) = R_Point(1)
        For pt = 1 To nPoints
            area = area + ((R_Point(pt).X * R_Point(pt + 1).Y) - (R_Point(pt + 1).X * R_Point(pt).Y)) / 2
        Next pt
        If area < 0 Then area = Math.Abs(area)
        'PolygonArea = area
        Return area
    End Function

    '{ID:GEO-014}
    Function MidLine(ByVal mLine As LineType) As POINTAPI
        MidLine.X = (mLine.Pt2.X + mLine.Pt1.X) / 2
        MidLine.Y = (mLine.Pt2.Y + mLine.Pt1.Y) / 2
        MidLine.Z = (mLine.Pt2.Z + mLine.Pt1.Z) / 2
        Return MidLine
    End Function

    '{ID:GEO-015}
    Public Function Line_Line(ByVal a As LineType, ByVal b As LineType, ByRef desx As Double, ByRef desy As Double) As Boolean
        On Error GoTo err1
        Dim A1 As Double
        Dim A2 As Double
        Dim b1 As Double
        Dim b2 As Double
        Dim C1 As Double
        Dim C2 As Double
        Dim F As Boolean = False

        a.Dx = a.Pt2.X - a.Pt1.X
        a.DY = a.Pt2.Y - a.Pt1.Y
        b.Dx = b.Pt2.X - b.Pt1.X
        b.DY = b.Pt2.Y - b.Pt1.Y

        A1 = a.Dx        'a.xt
        A2 = a.DY        'a.yt
        b1 = b.Dx * -1   '-b.xt
        b2 = b.DY * -1   '-b.yt
        C1 = a.Pt1.X - b.Pt1.X
        C2 = a.Pt1.Y - b.Pt1.Y
        Dim T, S
        T = 0
        S = 0
        If b1 = 0 Then
            If A1 <> 0 Then T = -C1 / A1
        Else
            T = ((b2 * C1) / b1 - C2) / (A2 - (b2 * A1) / b1)
        End If
        desx = a.Pt1.X + a.Dx * T
        desy = a.Pt1.Y + a.DY * T
        If Point_Line(desx, desy, a) And Point_Line(desx, desy, b) Then
            F = True '{GEO-016}
            Line_Line = F
            Return Line_Line
        End If

        Line_Line = F
        Return Line_Line

        Exit Function
err1:
        Line_Line = False
    End Function

    '{ID:GEO-016}
    Public Function Point_Line(ByVal X As Double, ByVal Y As Double, ByVal P As LineType) As Boolean
        Dim t1 As Double
        Dim t2 As Double
        Dim op As Boolean
        Dim T
        P.Dx = P.Pt2.X - P.Pt1.X
        P.DY = P.Pt2.Y - P.Pt1.Y
        If P.Dx = 0 Then
            T = (Y - P.Pt1.Y) / P.DY
            If T <= 1 And T >= 0 And X = P.Pt1.X Then op = True
        End If
        If P.DY = 0 Then
            T = (X - P.Pt1.X) / P.Dx
            If T <= 1 And T >= 0 And Y = P.Pt1.Y Then op = True
        End If
        If P.Dx <> 0 And P.DY <> 0 Then
            t1 = (X - P.Pt1.X) / P.Dx
            t2 = (Y - P.Pt1.Y) / P.DY
            If Math.Abs(t1 - t2) <= 0.5 And t1 <= 1 And t1 >= 0 Then op = True
        End If
        Point_Line = op
    End Function

    '{ID:GEO-017}

    Public Function PointInPolygon(ByVal Poly() As POINTAPI, ByVal Xray As Double, ByVal YofRay As Double) As Boolean
        Dim X As Long
        Dim PolyCount As Long
        Dim NumSidesCrossed As Long
        Dim LenOfSide As Double
        Dim CrossPt As POINTAPI
        CrossPt.X = Xray
        PolyCount = 1 + UBound(Poly) - LBound(Poly)
        For X = LBound(Poly) To UBound(Poly)
            If Poly(X).X > Xray Xor Poly((X + 1) Mod PolyCount).X > Xray Then
                CrossPt.Y = Y_at_X_Ray(Xray, Poly(X), Poly((X + 1) Mod PolyCount))
                If CrossPt.Y > YofRay Then
                    LenOfSide = PtDist(Poly(X), Poly((X + 1) Mod PolyCount))
                    If LenOfSide > PtDist(Poly(X), CrossPt) And _
                      LenOfSide > PtDist(Poly((X + 1) Mod PolyCount), CrossPt) Then
                        NumSidesCrossed = NumSidesCrossed + 1
                    End If
                End If
            End If
        Next
        If NumSidesCrossed Mod 2 Then PointInPolygon = True Else PointInPolygon = False
    End Function

    Public Function Y_at_X_Ray(ByVal Xray As Double, _
      ByVal p1 As POINTAPI, _
      ByVal p2 As POINTAPI) As Double
        Dim m As Double
        Dim b As Double
        m = (p2.Y - p1.Y) / (p2.X - p1.X)
        b = (p1.Y * p2.X - p1.X * p2.Y) / (p2.X - p1.X)
        Y_at_X_Ray = m * Xray + b
    End Function

    Private Function PtDist(ByVal p1 As POINTAPI, ByVal p2 As POINTAPI) As Double
        PtDist = Math.Sqrt((p2.Y - p1.Y) * (p2.Y - p1.Y) + _
        (p2.X - p1.X) * (p2.X - p1.X))
    End Function

    '{ID:GEO-018}
    Function Intersect(ByVal p1 As POINTAPI, ByVal p2 As POINTAPI, ByVal p3 As POINTAPI, ByVal p4 As POINTAPI) As Boolean
        Intersect = (((CCW(p1, p2, p3) * CCW(p1, p2, p4)) <= 0) And ((CCW(p3, p4, p1) * CCW(p3, p4, p2) <= 0)))  '{GEO-019}
    End Function

    '{ID:GEO-019}
    Function CCW(ByVal p0 As POINTAPI, ByVal p1 As POINTAPI, ByVal p2 As POINTAPI) As Long

        Dim dx1 As Long, dx2 As Long
        Dim dy1 As Long, dy2 As Long
        dx1 = p1.X - p0.X
        dx2 = p2.X - p0.X

        dy1 = p1.Y - p0.Y
        dy2 = p2.Y - p0.Y

        If (dx1 * dy2 > dy1 * dx2) Then
            CCW = 1
        Else
            CCW = -1
        End If
    End Function

    '{ID:GEO-020}
    Function ParallelLine(ByVal L1 As LineType, ByVal L2 As LineType) As Boolean
        ParallelLine = False
        Dim Ang1, Ang2
        Ang1 = AzmAngle(L1)                                  '{GEO-011}
        Ang2 = AzmAngle(L2)                                  '{GEO-011}
        If Math.Abs(Ang1 - Ang2) <= 1 Then ParallelLine = True
        If Math.Abs(Math.Abs(Ang1 - Ang2) - 180) <= 1 Then ParallelLine = True
        'frmEvalutionTable.grdGA4Mainline.TextMatrix(2, 10) = Format(Ang1, "0.00")
        'frmEvalutionTable.grdGA4Mainline.TextMatrix(3, 10) = Format(Ang2, "0.00")
    End Function

    '{ID:GEO-021}
    Function PerpendicularLine(ByVal L1 As LineType, ByVal L2 As LineType) As Boolean
        PerpendicularLine = False
        Dim Ang1, Ang2
        Ang1 = AzmAngle(L1)                                  '{GEO-011}
        Ang2 = AzmAngle(L2)                                  '{GEO-011}
        If Math.Abs(Math.Abs(Ang1 - Ang2) - 90) <= 1 Then PerpendicularLine = True
        If Math.Abs(Math.Abs(Ang1 - Ang2) - 270) <= 1 Then PerpendicularLine = True
    End Function

    '{ID:GEO-022}
    Function Offset(ByVal Dx As Double, ByVal p1 As POINTAPI, ByVal p2 As POINTAPI) As POINTAPI
        Dim C1 As CircleType
        Dim p3 As POINTAPI
        Dim LOffset As LineType
        Dim R As Double
        Dim Chk As Integer
        LOffset.Pt1 = p1
        LOffset.Pt2 = p2
        R = Length(LOffset)                                  '{GEO-005}
        C1.pt = p1
        C1.R = R + Dx
        LOffset.Pt1 = p1
        LOffset.Pt2 = DLengthPosition(p1, p2)                '{GEO-023}
        Chk = Line_Circle(LOffset, C1, Offset.X, Offset.Y, p3.X, p3.Y)  '{GEO-024}
        If Chk <= 0 Then
            Offset = p2
        End If
    End Function

    '{ID:GEO-023}
    Function DLengthPosition(ByVal p1 As POINTAPI, ByVal p2 As POINTAPI) As POINTAPI
        DLengthPosition.X = p1.X + (p2.X - p1.X) * 2
        DLengthPosition.Y = p1.Y + (p2.Y - p1.Y) * 2
    End Function

    '{ID:GEO-024}
    Public Function Line_Circle(ByVal P As LineType, ByVal k As CircleType, ByRef desx1 As Double, ByRef desy1 As Double, ByRef desx2 As Double, ByRef desy2 As Double) As Integer
        Dim a As Double
        Dim X1 As Double
        Dim x2 As Double
        Dim Y1 As Double
        Dim y2 As Double
        Dim point1_exist As Boolean
        Dim point2_exist As Boolean
        Dim points As Integer ' points of intersection
        Dim intersection As Boolean
        Dim Dx, DY, DR, d
        X1 = P.Pt1.X - k.pt.X
        x2 = P.Pt2.X - k.pt.X          'p.Pt2.x=(p.xn + p.xt)
        Y1 = P.Pt1.Y - k.pt.Y
        y2 = P.Pt2.Y - k.pt.Y          'p.Pt2.y=(p.yn + p.yt)
        Dx = x2 - X1
        DY = y2 - Y1
        DR = Math.Sqrt(Dx ^ 2 + DY ^ 2)
        d = X1 * y2 - x2 * Y1

        If (k.R ^ 2 * DR ^ 2 - d ^ 2) >= 0 Then intersection = True

        If intersection = True Then
            If (k.R ^ 2 * DR ^ 2 - d ^ 2) < 0 Then
                a = 0
            Else
                a = Math.Sqrt(k.R ^ 2 * DR ^ 2 - d ^ 2)
            End If
            desx1 = (d * DY + My_Sgn(DY) * Dx * a) / DR ^ 2 + k.pt.X     '{GEO-025}
            desy1 = (-d * Dx + Math.Abs(DY) * a) / DR ^ 2 + k.pt.Y
            desx2 = (d * DY - My_Sgn(DY) * Dx * a) / DR ^ 2 + k.pt.X     '{GEO-025}
            desy2 = (-d * Dx - Math.Abs(DY) * a) / DR ^ 2 + k.pt.Y
        End If
        point1_exist = Point_Line(desx1, desy1, P)                       '{GEO-016}
        point2_exist = Point_Line(desx2, desy2, P)                       '{GEO-016}
        If point1_exist And point2_exist Then
            points = 2
        Else
            points = 0
            If point1_exist Then points = 1
            If point2_exist Then points = 1 : desx1 = desx2 : desy1 = desy2
        End If

        Line_Circle = points

    End Function

    '{ID:GEO-025}
    Public Function My_Sgn(ByVal X) As Integer
        If X < 0 Then
            My_Sgn = -1
        Else
            My_Sgn = 1
        End If
    End Function

    '{ID:GEO-026}


    '{ID:GEO-027}
    Sub Perpend2Point(ByVal L1 As LineType, ByVal mp As POINTAPI, ByVal L2 As Double, ByRef LP As LineType)
        Dim Ang1
        Ang1 = AzmAngle(L1) + 90                             '{GEO-011}
        LP.Pt1.X = Math.Sin((Ang1) * PI / 180) * L2 + mp.X
        LP.Pt1.Y = Math.Cos((Ang1) * PI / 180) * L2 + mp.Y
        LP.Pt2.X = Math.Sin((Ang1 + 180) * PI / 180) * L2 + mp.X
        LP.Pt2.Y = Math.Cos((Ang1 + 180) * PI / 180) * L2 + mp.Y
    End Sub

    Sub Perpend2PointSingleArm(ByVal L1 As LineType, ByVal mp As POINTAPI, ByVal L2 As Double, ByVal arm As PerpendArm, ByRef LP As LineType)
        Dim Ang1
        Ang1 = AzmAngle(L1) + 90                             '{GEO-011}
        If arm = PerpendArm.Left Then
            LP.Pt1.X = mp.X 'Math.Sin((Ang1) * PI / 180) * L2 + mp.X
            LP.Pt1.Y = mp.Y 'Math.Cos((Ang1) * PI / 180) * L2 + mp.Y
            LP.Pt2.X = Math.Sin((Ang1 + 180) * PI / 180) * L2 + mp.X
            LP.Pt2.Y = Math.Cos((Ang1 + 180) * PI / 180) * L2 + mp.Y
        End If
        If arm = PerpendArm.Right Then
            LP.Pt2.X = Math.Sin((Ang1) * PI / 180) * L2 + mp.X
            LP.Pt2.Y = Math.Cos((Ang1) * PI / 180) * L2 + mp.Y
            LP.Pt1.X = mp.X 'Math.Sin((Ang1 + 180) * PI / 180) * L2 + mp.X
            LP.Pt1.Y = mp.Y 'Math.Cos((Ang1 + 180) * PI / 180) * L2 + mp.Y
        End If
    End Sub

    '{ID:GEO-028}
    Function PointTodist(ByVal Pth As PathType, ByVal pp1 As POINTAPI) As Double
        Dim LP As LineType
        Dim PLength As Double
        'Dim Chk As Boolean
        Dim Dist As Double
        Dim I
        Dist = 0
        PLength = PathLength(Pth)                        '{GEO-006}
        PointTodist = -1
        For I = 1 To Pth.nPt - 1
            LP.Pt1.X = Pth.pt(I).X
            LP.Pt1.Y = Pth.pt(I).Y
            LP.Pt2.X = Pth.pt(I + 1).X
            LP.Pt2.Y = Pth.pt(I + 1).Y
            If Point_Line(pp1.X, pp1.Y, LP) Then           '{GEO-016}
                LP.Pt2 = pp1
                PointTodist = Dist + Length(LP)              '{GEO-005}
                Exit Function
            Else
                Dist = Dist + Length(LP)                     '{GEO-005}
            End If
        Next
    End Function

    '{ID:GEO-029}
    Function pointOnBC(ByVal LL As LineType, ByVal bc As PathType, ByVal pp1 As POINTAPI, ByVal pp2 As POINTAPI) As Boolean
        Dim p1 As POINTAPI
        Dim p2 As POINTAPI, X, Y
        Dim LineEadge As LineType
        Dim I, j, k
        Dim check As Boolean
        check = True
        For I = 1 To bc.nPt - 1
            p1 = BcPath.pt(I)
            p2 = BcPath.pt(I + 1)
            LineEadge.Pt1 = p1
            LineEadge.Pt2 = p2
            If check = True Then
                If Line_Line(LL, LineEadge, X, Y) = True Then                       '{GEO-015}
                    pp1.X = X
                    pp1.Y = Y
                    check = False
                End If
            Else
                If Line_Line(LL, LineEadge, X, Y) = True Then                       '{GEO-015}
                    pp2.X = X
                    pp2.Y = Y
                    pointOnBC = True
                End If
            End If
        Next
    End Function
    '
    '  Find point on planted edge
    '  2008-11-18
    '{ID:GEO-030}
    Function DistToPoint(ByVal Pth As PathType, ByVal Dist As Double) As POINTAPI
        Dim LP As LineType
        Dim PLength As Double
        Dim PL1, PL2
        PLength = PathLength(Pth)         '{GEO-006}
        If Dist = 0 Or Dist = PLength Then
            DistToPoint = Pth.pt(1)
            Exit Function
        End If
        If Dist > 0 And Dist < PLength Then
            PL1 = 0
            PL2 = 0
            Dim I
            For I = 1 To Pth.nPt - 1
                LP.Pt1.X = Pth.pt(I).X
                LP.Pt1.Y = Pth.pt(I).Y
                LP.Pt2.X = Pth.pt(I + 1).X
                LP.Pt2.Y = Pth.pt(I + 1).Y
                PL2 = PL2 + Length(LP)                     '{GEO-005}
                If PL1 <= Dist And Dist < PL2 Then
                    Dim subDist
                    subDist = Dist - PL1
                    DistToPoint = PointInLine(LP, subDist)   '{GEO-008}
                    Exit Function
                End If
                PL1 = PL2
            Next
        Else
            'MsgBox "Function Error"
        End If

    End Function




    '********************************************************************************
    '********************************************************************************
    '
    ' UNDEFINE ID
    '
    '********************************************************************************
    '********************************************************************************



    Function diffZ(ByVal mLine As LineType) As Double
        diffZ = mLine.Pt2.Z - mLine.Pt1.Z
    End Function


    Function destinationPiont(ByVal ang As Double, ByVal OriginalPoint As POINTAPI, ByVal L As Double) As POINTAPI
        Dim angR
        angR = ang * PI / 180
        destinationPiont.Y = Math.Cos(angR) * L + OriginalPoint.Y
        destinationPiont.X = Math.Sin(angR) * L + OriginalPoint.X
        'destinationPiont.X = Math.sin(angR) * L + OriginalPoint.Y
    End Function
    '
    'Function for convert the angle from Azimut to 4 qurdant angle.
    '
    Function Azm2Qurdrant(ByVal Azm) As Double
        If Azm >= 0 And Azm < 90 Then Azm2Qurdrant = 90 - Azm
        If Azm >= 90 And Azm < 180 Then Azm2Qurdrant = 360 - (Azm - 90)
        If Azm >= 180 And Azm < 270 Then Azm2Qurdrant = 270 - (Azm - 180)
        If Azm >= 270 And Azm < 360 Then Azm2Qurdrant = 90 + (360 - Azm)
    End Function
    '
    'This function is for find the intersection point of 2 circles.
    '
    Public Function Circle_Circle(ByVal k As CircleType, ByVal L As CircleType, ByVal desx1 As Double, ByVal desy1 As Double, ByVal desx2 As Double, ByVal desy2 As Double) As Integer
        Dim d As Double
        Dim X As Double
        Dim Y As Double
        Dim points As Integer
        Dim Dx, DY
        Dx = k.pt.X - L.pt.X
        DY = k.pt.Y - L.pt.Y
        d = Math.Sqrt(Dx ^ 2 + DY ^ 2)
        If d <> 0 Then
            X = (d ^ 2 - L.R ^ 2 + k.R ^ 2) / (2 * d)
            If (k.R ^ 2 - X ^ 2) > 0 Then Y = Math.Sqrt(k.R ^ 2 - X ^ 2)
            desx1 = k.pt.X - (X * (Dx / d) + Y * (DY / d))
            desy1 = k.pt.Y - (-Y * (Dx / d) + X * (DY / d))
            desx2 = k.pt.X - (X * (Dx / d) - Y * (DY / d))
            desy2 = k.pt.Y - (Y * (Dx / d) + X * (DY / d))
        End If
        If d < (k.R + L.R) And d > Math.Abs(k.R - L.R) Then points = 2
        If d = (k.R + L.R) Then points = 1

        Circle_Circle = points

    End Function

    Public Function DoyX(ByVal Ang As Double, ByVal x0 As Double, ByVal r As Double) As Double
        Dim Doy As Double = r * Math.Cos(Ang * Math.PI / 180) + x0
        Return Doy
    End Function

    Public Function DoyY(ByVal Ang As Double, ByVal y0 As Double, ByVal r As Double) As Double
        Dim Doy As Double = r * Math.Sin(Ang * Math.PI / 180) + y0
        Return Doy
    End Function

    Public Sub Arrow(ByVal x1 As Double, ByVal y1 As Double, ByVal x2 As Double, ByVal y2 As Double, ByVal Ang As Double, ByVal arrowratio As Double, ByRef x3 As Double, ByRef y3 As Double, ByRef x4 As Double, ByRef y4 As Double)
        'On Error GoTo 100
        Dim slope As Double
        If Ang = 0 Then
            Ang = 0.01
        End If
        If x1 = x2 Then
            slope = 0
        Else
            slope = m(x1, y1, x2, y2)
        End If
        Dim c, LL, arrowH, arrowB As Double
        c = lineConst(x1, y1, slope)
        ' arrow Length
        LL = L(x1, y1, x2, y2)
        'Arrow head 
        arrowH = LL / arrowratio

        arrowB = arrowH * Math.Tan(Ang * Math.PI / 180)
        Dim y5, x5 As Double
        x5 = (x2 - x1) * (arrowratio - 1) / arrowratio + x1
        y5 = (y2 - y1) * (arrowratio - 1) / arrowratio + y1

        If (x1 = x2) Or (y1 = y2) Then

            If (x1 = x2) Then
                x3 = x1 + arrowB
                x4 = x1 - arrowB
                y3 = y5
                y4 = y5
            End If
            If (y1 = y2) Then
                x3 = x5
                x4 = x5
                y3 = y1 + arrowB
                y4 = y1 - arrowB
            End If

        Else
            If slope = 0 Then
                MsgBox("Slope = 0 ")
            End If
            Dim antis, antic, a, b, cc As Double ', x3, x4, y3, y4
            antis = -1 / slope
            antic = lineConst(x5, y5, antis)
            a = (1 + antis ^ 2)
            b = (2 * antis * antic - 2 * antis * y5 - 2 * x5)
            cc = (x5 ^ 2 + y5 ^ 2 - 2 * y5 * antic + antic ^ 2 - arrowB ^ 2)
            x3 = (-b + Math.Abs((b ^ 2 - 4 * a * cc)) ^ 0.5) / 2 / a
            x4 = (-b - Math.Abs((b ^ 2 - 4 * a * cc)) ^ 0.5) / 2 / a
            y3 = x3 * antis + antic
            y4 = x4 * antis + antic
        End If

100:    Exit Sub
    End Sub

    Public Sub Arrow(ByVal L1 As LineType, ByVal Ang As Double, ByVal arrowratio As Double, ByRef x3 As Double, ByRef y3 As Double, ByRef x4 As Double, ByRef y4 As Double)
        Dim x1 As Double = L1.Pt1.X
        Dim y1 As Double = L1.Pt1.Y
        Dim x2 As Double = L1.Pt2.X
        Dim y2 As Double = L1.Pt2.Y
        'On Error GoTo 100
        Dim slope As Double
        If Ang = 0 Then
            Ang = 0.01
        End If
        If x1 = x2 Then
            slope = 0
        Else
            slope = m(x1, y1, x2, y2)
        End If
        Dim c, LL, arrowH, arrowB As Double
        c = lineConst(x1, y1, slope)
        ' arrow Length
        LL = L(x1, y1, x2, y2)
        'Arrow head 
        arrowH = LL / arrowratio

        arrowB = arrowH * Math.Tan(Ang * Math.PI / 180)
        Dim y5, x5 As Double
        x5 = (x2 - x1) * (arrowratio - 1) / arrowratio + x1
        y5 = (y2 - y1) * (arrowratio - 1) / arrowratio + y1

        If (x1 = x2) Or (y1 = y2) Then

            If (x1 = x2) Then
                x3 = x1 + arrowB
                x4 = x1 - arrowB
                y3 = y5
                y4 = y5
            End If
            If (y1 = y2) Then
                x3 = x5
                x4 = x5
                y3 = y1 + arrowB
                y4 = y1 - arrowB
            End If

        Else
            If slope = 0 Then
                MsgBox("Slope = 0 ")
            End If
            Dim antis, antic, a, b, cc As Double ', x3, x4, y3, y4
            antis = -1 / slope
            antic = lineConst(x5, y5, antis)
            a = (1 + antis ^ 2)
            b = (2 * antis * antic - 2 * antis * y5 - 2 * x5)
            cc = (x5 ^ 2 + y5 ^ 2 - 2 * y5 * antic + antic ^ 2 - arrowB ^ 2)
            x3 = (-b + Math.Abs((b ^ 2 - 4 * a * cc)) ^ 0.5) / 2 / a
            x4 = (-b - Math.Abs((b ^ 2 - 4 * a * cc)) ^ 0.5) / 2 / a
            y3 = x3 * antis + antic
            y4 = x4 * antis + antic
        End If

100:    Exit Sub
    End Sub

    Public Function m(ByVal x1 As Double, ByVal y1 As Double, ByVal x2 As Double, ByVal y2 As Double) As Double
        On Error Resume Next
        m = (y2 - y1) / (x2 - x1)
        Return m
    End Function

    Public Function lineConst(ByVal x1 As Double, ByVal y1 As Double, ByVal m As Double) As Double
        On Error Resume Next
        lineConst = y1 - m * x1
        Return lineConst
    End Function

    Public Function L(ByVal x1 As Double, ByVal y1 As Double, ByVal x2 As Double, ByVal y2 As Double) As Double
        Dim dx, dy As Double
        dx = (x1 - x2)
        dy = (y1 - y2)
        L = (dx ^ 2 + dy ^ 2) ^ 0.5
        Return L
    End Function

    Public Function Rx(ByVal x1 As Double, ByVal y1 As Double, ByVal Ang As Double) As Double
        Dim angR As Double = Math.PI * Ang / 180
        Rx = x1 * Math.Cos(angR) + y1 * Math.Sin(angR)
        Return Rx
    End Function

    Public Function Ry(ByVal x1 As Double, ByVal y1 As Double, ByVal Ang As Double) As Double
        Dim angR As Double = Math.PI * Ang / 180
        Ry = -x1 * Math.Sin(angR) + y1 * Math.Cos(angR)
        Return Ry
    End Function


    Public Function colorScale(ByVal Z As Double) As System.Drawing.Color
        Dim r, g, b As Int32
        Dim Zval As Double
        r = 255
        g = 0
        b = 0
        'Assume Max wave height = 3 m
        Zval = CInt(Z * 6)
        If Zval = 0 Then r = 255 : g = 1 : b = 0
        If Zval = 1 Then r = 255 : g = 32 : b = 0
        If Zval = 2 Then r = 255 : g = 59 : b = 2
        If Zval = 3 Then r = 255 : g = 88 : b = 17
        If Zval = 4 Then r = 255 : g = 115 : b = 38
        If Zval = 5 Then r = 255 : g = 139 : b = 64
        If Zval = 6 Then r = 255 : g = 161 : b = 94
        If Zval = 7 Then r = 255 : g = 181 : b = 125
        If Zval = 8 Then r = 255 : g = 198 : b = 158
        If Zval = 9 Then r = 255 : g = 213 : b = 190
        If Zval = 10 Then r = 255 : g = 226 : b = 221
        If Zval = 11 Then r = 255 : g = 238 : b = 251
        If Zval = 12 Then r = 231 : g = 226 : b = 255
        If Zval = 13 Then r = 210 : g = 213 : b = 255
        If Zval = 14 Then r = 194 : g = 204 : b = 255
        If Zval = 15 Then r = 181 : g = 195 : b = 255
        If Zval = 16 Then r = 170 : g = 188 : b = 255
        If Zval = 17 Then r = 161 : g = 182 : b = 255
        If Zval >= 18 Then r = 153 : g = 176 : b = 255

        colorScale = System.Drawing.Color.FromArgb(0, 0, r)

        Return colorScale
    End Function

#Region "Distance point to line"

    Public Function lineMagnitude(ByVal x1 As Double, ByVal y1 As Double, ByVal x2 As Double, ByVal y2 As Double) As Double
        lineMagnitude = ((x2 - x1) ^ 2 + (y2 - y1) ^ 2) ^ 0.5
    End Function

    Public Function DistancePointLine(ByVal px As Double, ByVal py As Double, ByVal x1 As Double, ByVal y1 As Double, _
                                      ByVal x2 As Double, ByVal y2 As Double, ByRef xx As Double, ByRef yy As Double) As Double
        ' Returns 9999 on 0 denominator conditions.
        Dim LineMag As Double, u As Double
        Dim ix As Double, iy As Double ' intersecting point

        LineMag = lineMagnitude(x1, y1, x2, y2)
        If LineMag < 0.00000001 Then
            xx = px
            yy = py
            DistancePointLine = 99999999 : Exit Function
        End If

        xx = px
        yy = py
        u = (((px - x1) * (x2 - x1)) + ((py - y1) * (y2 - y1)))
        u = u / (LineMag * LineMag)
        If u < 0.00001 Or u > 1 Then
            '// closest point does not fall within the line segment, take the shorter distance
            '// to an endpoint
            ix = lineMagnitude(px, py, x1, y1)
            iy = lineMagnitude(px, py, x2, y2)
            If ix > iy Then DistancePointLine = iy Else DistancePointLine = ix
        Else
            ' Intersecting point is on the line, use the formula
            ix = x1 + u * (x2 - x1)
            iy = y1 + u * (y2 - y1)
            DistancePointLine = lineMagnitude(px, py, ix, iy)
        End If
        xx = ix
        yy = iy
    End Function
#End Region

#Region "Creat shapefile"

    Function newShoreShp(ByVal filename As String, ByRef Lnd As Geo_Function.PathType, ByRef Sea As Geo_Function.PathType) As Boolean
        On Error GoTo Error_handler
        Dim success As Boolean
        Dim fieldindex As Long
        Dim shapeIndex As Long
        Dim data As Integer
        'objects
        Dim sf As MapWinGIS.Shapefile
        Dim field1 As MapWinGIS.Field
        Dim mwField As MapWinGIS.Field
        Dim LandShape As MapWinGIS.Shape
        Dim point As MapWinGIS.Point

        'Create a new polygon shapefile
        sf = New MapWinGIS.Shapefile
        With sf
            success = .CreateNew(filename, MapWinGIS.ShpfileType.SHP_POLYGON)
            If Not success Then MsgBox("Error in creating shapefile: " & .ErrorMsg(.LastErrorCode))
        End With 'sf

        'Start Editing it...
        success = sf.StartEditingShapes(True)

        'After a shapefile is created, the attribute table and
        'shapefile are automatically in editing mode

        mwField = New MapWinGIS.Field
        mwField.Name = "MWShapeID"
        mwField.Width = 8
        mwField.Type = MapWinGIS.FieldType.INTEGER_FIELD

        'At least one field is required in the table to be a valid shapefile.

        '---------------------------------------------------------------------
        'Pipe Network data
        field1 = New MapWinGIS.Field
        field1.Name = "Predict"
        field1.Width = 16
        field1.Type = MapWinGIS.FieldType.STRING_FIELD
        With sf
            success = .EditInsertField(mwField, 0)
            success = .EditInsertField(field1, 0)
            If Not success Then
                MsgBox("Error in adding field: " & .ErrorMsg(.LastErrorCode))
                GoTo Error_handler
            End If
        End With ' sf

        shapeIndex = 0
        point = New MapWinGIS.Point
        Dim pnt As Integer = 0
        Dim x(1) As Double
        Dim y(1) As Double
        Dim Node1, Node2 As Integer
        LandShape = New MapWinGIS.Shape
        'Create a new Point shape
        With LandShape
            success = .Create(MapWinGIS.ShpfileType.SHP_POLYGON)
            If Not success Then
                MsgBox("Error in creating shape: " & .ErrorMsg(.LastErrorCode))
                GoTo Error_handler
            End If
        End With ' shape
        LandShape.ShapeType = MapWinGIS.ShpfileType.SHP_POLYGON

        '-----------------------------------------------------------------------------------
        'Create Land area
        '-----------------------------------------------------------------------------------
        'Set up the points
        'Insert the point into the shape
        For pnt = 1 To Lnd.nPt
            point = New MapWinGIS.Point
            point.x = Lnd.pt(pnt).X
            point.y = Lnd.pt(pnt).Y
            'Add the points to a shape
            success = LandShape.InsertPoint(point, pnt - 1)
            If Not success Then
                MsgBox("Error in adding point: " & LandShape.ErrorMsg(LandShape.LastErrorCode))
                GoTo Error_handler
            End If
        Next pnt

        'Insert the shape into the shapefile
        With sf
            success = .EditInsertShape(LandShape, shapeIndex)
            If Not success Then
                MsgBox("Error in adding field: " & .ErrorMsg(.LastErrorCode))
                GoTo Error_handler
            End If
        End With ' sf

        'Add value to at least one attribute
        'Use shapeindex as dummy value:
        data = shapeIndex
        Dim ii As Integer = 0

        With sf
            success = .EditCellValue(0, shapeIndex, "Land")                   'Mapwin ID
            If Not success Then
                MsgBox("Error in adding field: " & .ErrorMsg(.LastErrorCode))
                GoTo Error_handler
            End If
        End With ' sf


        '-----------------------------------------------------------------------------------
        'Create Sea area
        '-----------------------------------------------------------------------------------
        'Set up the points
        'Insert the point into the shape
        shapeIndex = 1
        Dim SeaShape As MapWinGIS.Shape
        SeaShape = New MapWinGIS.Shape
        'Create a new Point shape
        With SeaShape
            success = .Create(MapWinGIS.ShpfileType.SHP_POLYGON)
            If Not success Then
                MsgBox("Error in creating shape: " & .ErrorMsg(.LastErrorCode))
                GoTo Error_handler
            End If
        End With ' shape
        SeaShape.ShapeType = MapWinGIS.ShpfileType.SHP_POLYGON
        For pnt = 1 To Sea.nPt
            point = New MapWinGIS.Point
            point.x = Sea.pt(pnt).X
            point.y = Sea.pt(pnt).Y
            'Add the points to a shape
            success = SeaShape.InsertPoint(point, pnt - 1)
            If Not success Then
                MsgBox("Error in adding point: " & SeaShape.ErrorMsg(SeaShape.LastErrorCode))
                GoTo Error_handler
            End If
        Next pnt
        'Insert the shape into the shapefile
        With sf
            success = .EditInsertShape(SeaShape, shapeIndex)
            If Not success Then
                MsgBox("Error in adding field: " & .ErrorMsg(.LastErrorCode))
                GoTo Error_handler
            End If
        End With ' sf

        'Add value to at least one attribute
        'Use shapeindex as dummy value:
        data = shapeIndex
        With sf
            success = .EditCellValue(0, shapeIndex, "Sea")                   'Mapwin ID
            If Not success Then
                MsgBox("Error in adding field: " & .ErrorMsg(.LastErrorCode))
                GoTo Error_handler
            End If
        End With ' sf

        'Stop editing shapes in the shapefile, saving changes to shapes,
        'also stopping editing of the attribute table
        With sf
            success = sf.StopEditingShapes(True, True)
            If Not success Then
                MsgBox("Error in adding field: " & .ErrorMsg(.LastErrorCode))
                GoTo Error_handler
            End If
        End With ' sf

        newShoreShp = True

        'MsgBox("Complete")
Cleanup:
        On Error Resume Next
        field1 = Nothing
        LandShape = Nothing
        point = Nothing
        sf = Nothing

        Exit Function

Error_handler:
        newShoreShp = False
        GoTo Cleanup
    End Function


#End Region
End Module
