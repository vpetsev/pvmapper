using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVDESKTOP
{
    class SolarCal
    {
       /* public  DateTime Date_;//{ get; set; }
        public  double Latitude;//{ get; set; }
        public  double Longitude;//{ get; set; }
        public Int16 TimeZone_;// { get; set; }
        public double TimePassMidnight;// { get {} set { } }
       */
        //-----------------------------------------------------
    #region internal variable
        private double _JulianDay = 0;
        private double _JulianCentury = 0;
        private double _GeomMeanLongSun = 0;
        private double _GeomMeanAnomSun = 0;
        private double _EccentEarthOrbit = 0;
        private double _SunEqofCtr = 0;
        private double _SunTrueLong = 0;
        private double _SunTrueAnom = 0;
        private double _SunRadVector = 0;
        private double _SunAppLong = 0;
        private double _MeanObliqEcliptic = 0;
        private double _ObliqCorr = 0;
        private double _SunRtAscen = 0;
        private double _SunDeclin = 0;
        private double _varY = 0;
        private double _EqOfTime_min = 0;
        private double _HASunrise = 0;
        private double _SolarNoon = 0;
        private double _SunriseTime = 0;
        private double _SunsetTime = 0;
        private double _SunlightDuration_min = 0;
        private double _TrueSolarTime_min = 0;
        private double _HourAngle = 0;
        private double _SolarZenithAngle = 0;
        private double _SolarElevationAngle = 0;
        private double _ApproxAtmosphericRefraction = 0;
        private double _SolarElevationCorrectedForAtmRefraction = 0;
        private double _SolarAzimuthAngle = 0;
    #endregion

    #region External variable
        public double JulianDay { get { return _JulianDay; } set { } }
        public double JulianCentury { get { return _JulianCentury; } set { } }
        public double GeomMeanLongSun { get { return _GeomMeanLongSun; } set { } }
        public double GeomMeanAnomSun { get { return _GeomMeanAnomSun; } set { } }
        public double EccentEarthOrbit { get { return _EccentEarthOrbit; } set { } }
        public double SunEqofCtr { get { return _SunEqofCtr; } set { } }
        public double SunTrueLong { get { return _SunTrueLong; } set { } }
        public double SunTrueAnom { get { return _SunTrueAnom; } set { } }
        public double SunRadVector { get { return _SunRadVector; } set { } }
        public double SunAppLong { get { return _SunAppLong; } set { } }
        public double MeanObliqEcliptic { get { return _MeanObliqEcliptic; } set { } }
        public double ObliqCorr { get { return _ObliqCorr; } set { } }
        public double SunRtAscen { get { return _SunRtAscen; } set { } }
        public double SunDeclin { get { return _SunDeclin; } set { } }
        public double varY { get { return _varY; } set { } }
        public double EqOfTime_min { get { return _EqOfTime_min; } set { } }
        public double HASunrise { get { return _HASunrise; } set { } }
        public double SolarNoon { get { return _SolarNoon; } set { } }
        public double SunriseTime { get { return _SunriseTime; } set { } }
        public double SunsetTime { get { return _SunsetTime; } set { } }
        public double SunlightDuration_min { get { return _SunlightDuration_min; } set { } }
        public double TrueSolarTimemin { get { return _TrueSolarTime_min; } set { } }
        public double HourAngle { get { return _HourAngle; } set { } }
        public double SolarZenithAngle { get { return _SolarZenithAngle; } set { } }
        public double SolarElevationAngle { get { return _SolarElevationAngle; } set { } }
        public double ApproxAtmosphericRefraction { get { return _ApproxAtmosphericRefraction; } set { } }
        public double SolarElevationCorrectedForAtmRefraction { get { return _SolarElevationCorrectedForAtmRefraction; } set { } }
        public double SolarAzimuthAngle { get { return _SolarAzimuthAngle; } set { } }
    #endregion

    #region main function
        // 01 getJulianDay
        internal static double getJulianDay(DateTime dateTime, int TimeZone, double timeAffterMidnight)
        {
            int month = dateTime.Month;
            int day = dateTime.Day;
            int year = dateTime.Year;
            long JD = DataToJuliana(dateTime); 
            
            double JulianDay = JD  + timeAffterMidnight - (Double)TimeZone / 24;
            return JulianDay;
        }

        internal static double getJulianDay(int day, int month, int year,  int TimeZone, double timeAffterMidnight)
        {
            long JD = DataToJuliana(day,month,year);

            double JulianDay = JD + timeAffterMidnight - (Double)TimeZone / 24;
            return JulianDay;
        }
        #region Julian Date

            public static DateTime JulianaToDateTime(double julianDate)
            {
                DateTime date;
                double dblA, dblB, dblC, dblD, dblE, dblF;
                double dblZ, dblW, dblX;
                int day, month, year;
                try
                {
                    dblZ = Math.Floor(julianDate + 0.5);
                    dblW = Math.Floor((dblZ - 1867216.25) / 36524.25);
                    dblX = Math.Floor(dblW / 4);
                    dblA = dblZ + 1 + dblW - dblX;
                    dblB = dblA + 1524;
                    dblC = Math.Floor((dblB - 122.1) / 365.25);
                    dblD = Math.Floor(365.25 * dblC);
                    dblE = Math.Floor((dblB - dblD) / 30.6001);
                    dblF = Math.Floor(30.6001 * dblE);
                    day = Convert.ToInt32(dblB - dblD - dblF);
                    if (dblE > 13)
                    {
                        month = Convert.ToInt32(dblE - 13);
                    }
                    else
                    {
                        month = Convert.ToInt32(dblE - 1);
                    }

                    if (month == 2)
                    {
                        year = Convert.ToInt32(dblC - 4715);
                    }

                    else
                    {
                        year = Convert.ToInt32(dblC - 4716);
                    }
                    if (month == 1)
                    {
                        year = Convert.ToInt32(dblC - 4715);
                    }
                    date = new DateTime(year, month, day);
                    return date;
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    date = new DateTime(0);
                }
                catch (Exception ex)
                {
                    date = new DateTime(0);
                }
                return date;
            }
            public static long DataToJuliana(DateTime dt)
            {
                return DataToJuliana(dt.Day, dt.Month, dt.Year);
            }
            public static long DataToJuliana(int d, int m, int y)
            {
                return (1461 * (y + 4800 + (m - 14) / 12)) / 4 +
                                (367 * (m - 2 - 12 * ((m - 14) / 12))) / 12 -
                                (3 * ((y + 4900 + (m - 14) / 12) / 100)) / 4 + d - 32075;

            }

        #endregion

            //02 getJulianCentury
            internal static double getJulianCentury(double JulianDayVal_)
            {
                double a = 2451545.0;
                double b = 36525.0;
                return (JulianDayVal_ - a) / b;
                //JulianCentury = (JulianDayVal - 2451545#) / 36525#

            }
            //03 getGeomMeanLongSun
            internal static double getGeomMeanLongSun(double JulianCentury_)
            {
                return kUtility.myMOD(280.46646 + JulianCentury_ * (36000.76983 + JulianCentury_ * 0.0003032), 360);
                //  = myMOD2(280.46646 + JulianCentury_ * (36000.76983 + JulianCentury_ * 0.0003032), 360)
            }
            //04 getGeomMeanAnomSun
            internal static double getGeomMeanAnomSun(double JulianCentury_)
            {
	            return 357.52911 + JulianCentury_ * (35999.05029 - 0.0001537 * JulianCentury_);
            }
            //05 getEccentEarthOrbit
            internal static double getEccentEarthOrbit(double JulianCentury_)
            {
	            return 0.016708634 - JulianCentury_ * (4.2037E-05 + 1.267E-07 * JulianCentury_);
            }
            //06 getSunEqofCtr
            internal static double getSunEqofCtr(double JulianCentury_, double GeomMeanAnomSun_)
            {
                return Math.Sin(kUtility.Radians(GeomMeanAnomSun_)) * (1.914602 - JulianCentury_ * (0.004817 + 1.4E-05 * JulianCentury_)) + Math.Sin(kUtility.Radians(2 * GeomMeanAnomSun_)) * (0.019993 - 0.000101 * JulianCentury_) + Math.Sin(kUtility.Radians(3 * GeomMeanAnomSun_)) * 0.000289;
            }
            //07 getSunTrueLong
            internal static double getSunTrueLong(double GeomMeanLongSun_, double SunEqofCtr_)
            {
	            return GeomMeanLongSun_ + SunEqofCtr_;
            }
            //08 getSunTrueAnom
            internal static double getSunTrueAnom(double GeomMeanAnomSun_, double SunEqofCtr_)
            {
	            return GeomMeanAnomSun_ + SunEqofCtr_;
            }
            //09 getSunRadVector
            internal static double getSunRadVector(double EccentEarthOrbit_, double SunTrueAnom_)
            {
	            return (1.000001018 * (1 - EccentEarthOrbit_ * EccentEarthOrbit_)) / (1 + EccentEarthOrbit_ * Math.Cos(kUtility.Radians(SunTrueAnom_)));
            }
            //10 getSunAppLong
            internal static double getSunAppLong(double JulianCentury_, double SunTrueLong_)
            {
	            return SunTrueLong_ - 0.00569 - 0.00478 * Math.Sin(kUtility.Radians(125.04 - 1934.136 * JulianCentury_));
            }
            //11 getMeanObliqEcliptic
            internal static double getMeanObliqEcliptic(double JulianCentury_)
            {
	            double A1 = (0.00059 - JulianCentury_ * 0.001813);
	            return 23.0 + (26.0 + ((21.448 - JulianCentury_ * (46.815 + JulianCentury_ * A1))) / 60.0) / 60.0;
            }
            //12 getObliqCorr
            internal static double getObliqCorr(double JulianCentury_, double MeanObliqEcliptic_)
            {
	            return MeanObliqEcliptic_ + 0.00256 * Math.Cos(kUtility.Radians(125.04 - 1934.136 * JulianCentury_));
            }
            //13 getSunRtAscen
            internal static double getSunRtAscen(double SunAppLong_, double ObliqCorr_)
            {
                return kUtility.Degrees(Math.Atan2(Math.Cos(kUtility.Radians(SunAppLong_)), Math.Cos(kUtility.Radians(ObliqCorr_)) * Math.Sin(kUtility.Radians(SunAppLong_))));
            }
            //14 getSunDeclin
            internal static double getSunDeclin(double SunAppLong_, double ObliqCorr_)
            {
	            return kUtility. Degrees(Math.Asin(Math.Sin(kUtility.Radians(ObliqCorr_)) * Math.Sin(kUtility.Radians(SunAppLong_))));
            }
            //15 getvarY
            internal static double getvarY(double ObliqCorr_)
            {
	            return Math.Tan(kUtility.Radians(ObliqCorr_ / 2)) * Math.Tan(kUtility.Radians(ObliqCorr_ / 2));
            }
            //16 getEqOfTime_min
            internal static double getEqOfTime_min(double GeomMeanLongSun_, double GeomMeanAnomSun_, double EccentEarthOrbit_, double varY_)
            {
                double U2 = varY_;
                double I2 = GeomMeanLongSun_;
                double J2 = GeomMeanAnomSun_;
                double k2 = EccentEarthOrbit_;
	            return 4 * kUtility.Degrees(U2 * Math.Sin(2 * kUtility.Radians(I2)) - 2 * k2 * Math.Sin(kUtility.Radians(J2)) 
                     + 4 * k2 * U2 * Math.Sin(kUtility.Radians(J2)) * Math.Cos(2 * kUtility.Radians(I2)) 
                     - 0.5 * U2 * U2 * Math.Sin(4 * kUtility.Radians(I2)) 
                     - 1.25 * k2 * k2 * Math.Sin(2 * kUtility.Radians(J2)));
            }
            //17 getHASunrise
            internal static double getHASunrise(double Latitude, double SunDeclin_)
            {
                return kUtility.Degrees(Math.Acos(Math.Cos(kUtility.Radians(90.833))
                    / (Math.Cos(kUtility.Radians(Latitude)) * Math.Cos(kUtility.Radians(SunDeclin_)))
                    - Math.Tan(kUtility.Radians(Latitude)) * Math.Tan(kUtility.Radians(SunDeclin_))));
            }
            //18 getSolarNoon
            internal static double getSolarNoon(double Longitude, int TimeZone, double EqOfTime_min_)
            {
	            return (720 - 4 * Longitude - EqOfTime_min_ + TimeZone * 60) / 1440;
            }
            //19getSunriseTime
            internal static double getSunriseTime(double HASunrise_, double SolarNoon_)
            {
	            return SolarNoon_ - HASunrise_ * 4 / 1440;
            }
            //20 getSunsetTime
            internal static double getSunsetTime(double HASunrise_, double SolarNoon_)
            {
	            return SolarNoon_ + HASunrise_ * 4 / 1440;
            }
            //21 getSunlightDuration_min
            internal static double getSunlightDuration_min(double HASunrise_)
            {
	            return HASunrise_ * 8;
            }
            //22 getTrueSolarTime_min
            internal static double getTrueSolarTime_min(double Longitude, int TimeZone, double TimePassMidnight, double EqOfTime_min_)
            {
	            return  kUtility.myMOD(TimePassMidnight * 1440 + EqOfTime_min_ + 4 * Longitude - 60 * TimeZone, 1440);
            }
            //23 getHourAngle
            internal static double getHourAngle(double TrueSolarTime_min_)
            {
                if ((TrueSolarTime_min_ / 4 < 0))
                {
		            return  TrueSolarTime_min_ / 4 + 180;
	            } else {
		            return  TrueSolarTime_min_ / 4 - 180;
	            }
            }
            //24 getSolarZenithAngle
            internal static double getSolarZenithAngle(double Latitude, double SunDeclin_, double HourAngle_)
            {
                return kUtility.Degrees(Math.Acos(Math.Sin(kUtility.Radians(Latitude)) * Math.Sin(kUtility.Radians(SunDeclin_))
                     + Math.Cos(kUtility.Radians(Latitude)) * Math.Cos(kUtility.Radians(SunDeclin_)) * Math.Cos(kUtility.Radians(HourAngle_))));
            }
            //25 getSolarElevationAngle
            internal static double getSolarElevationAngle(double SolarZenithAngle_)
            {
	            return 90 - SolarZenithAngle_;
            }
            //26 getApproxAtmosphericRefraction
            internal static double getApproxAtmosphericRefraction(double SolarElevationAngle_)
            {
	            double functionReturnValue = 0;
                double AE2 = SolarElevationAngle_;
	            if (AE2 > 85) {
		            functionReturnValue = 0;
	            } else {
		            if (AE2 > 5) {
                        functionReturnValue = 58.1 / Math.Tan(kUtility.Radians(AE2)) - 0.07 / Math.Pow(Math.Tan(kUtility.Radians(AE2)), 3) 
                            + 8.6E-05 / Math.Pow((Math.Tan(kUtility.Radians(AE2))), 5);
		            } else {
			            if (AE2 > -0.575) {
				            functionReturnValue = 1735 + AE2 * (-518.2 + AE2 * (103.4 + AE2 * (-12.79 + AE2 * 0.711)));
			            } else {
				            functionReturnValue = -20.772 / Math.Tan(kUtility.Radians(AE2));
			            }
		            }
	            }
	            functionReturnValue = functionReturnValue / 3600;
	            return functionReturnValue;
            }
            //27 getSolarElevationCorrectedForAtmRefraction
            internal static double getSolarElevationCorrectedForAtmRefraction(double SolarElevationAngleVale, double ApproxAtmosphericRefractionVale)
            {
	            return SolarElevationAngleVale + ApproxAtmosphericRefractionVale;
            }
            //28 getSolarAzimuthAngle
            internal static double getSolarAzimuthAngle(double Latitude, double SunDeclin_, double HourAngle_, double SolarZenithAngle)
            {
	            double functionReturnValue = 0;
	            //IF(AC2>0,MOD(DEGREES(ACOS(((SIN(RADIANS($B$3))*COS(RADIANS(AD2)))-SIN(RADIANS(T2)))/(COS(RADIANS($B$3))*SIN(RADIANS(AD2)))))+180,360),MOD(540-DEGREES(ACOS(((SIN(RADIANS($B$3))*COS(RADIANS(AD2)))-SIN(RADIANS(T2)))/(COS(RADIANS($B$3))*SIN(RADIANS(AD2))))),360))
	            //
	            double T2 = SunDeclin_;
	            double AC2 = HourAngle_;
	            double AD2 = SolarZenithAngle;
	            if (AC2 > 0) {
		            functionReturnValue = kUtility.myMOD(kUtility.Degrees(Math.Acos(((Math.Sin(kUtility.Radians(Latitude)) * Math.Cos(kUtility.Radians(AD2))) 
                        - Math.Sin(kUtility.Radians(T2))) / (Math.Cos(kUtility.Radians(Latitude)) * Math.Sin(kUtility.Radians(AD2))))) + 180, 360);
	            } else {
		            functionReturnValue = kUtility.myMOD(540 - kUtility.Degrees(Math.Acos(((Math.Sin(kUtility.Radians(Latitude)) * Math.Cos(kUtility.Radians(AD2)))
                        - Math.Sin(kUtility.Radians(T2))) / (Math.Cos(kUtility.Radians(Latitude)) * Math.Sin(kUtility.Radians(AD2))))), 360);
	            }
	            if (functionReturnValue > 360)
		            functionReturnValue = functionReturnValue - 360;
	            if (functionReturnValue < 0)
		            functionReturnValue = 360 + functionReturnValue;
	            return functionReturnValue;
            }
    #endregion

        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------
        // Classs Method
            public SolarCal(DateTime date, double TimePassMidnight, double Lat, double Lng, Int16 TimeZone)
            {
                _JulianDay = SolarCal.getJulianDay(date, TimeZone, TimePassMidnight);
                _JulianCentury = SolarCal.getJulianCentury(_JulianDay);
                _GeomMeanLongSun = SolarCal.getGeomMeanLongSun(_JulianCentury);
                _GeomMeanAnomSun = SolarCal.getGeomMeanAnomSun(_JulianCentury);
                _EccentEarthOrbit = SolarCal.getEccentEarthOrbit(_JulianCentury);
                _SunEqofCtr = SolarCal.getSunEqofCtr(_JulianCentury, _GeomMeanAnomSun);
                _SunTrueLong = SolarCal.getSunTrueLong(_GeomMeanLongSun, _SunEqofCtr);
                _SunTrueAnom = SolarCal.getSunTrueAnom(_GeomMeanAnomSun, _SunEqofCtr);
                _SunRadVector = SolarCal.getSunRadVector(_EccentEarthOrbit, _SunTrueAnom);
                _SunAppLong = SolarCal.getSunAppLong(_JulianCentury, _SunTrueLong);
                _MeanObliqEcliptic = SolarCal.getMeanObliqEcliptic(_JulianCentury);
                _ObliqCorr = SolarCal.getObliqCorr(_JulianCentury, _MeanObliqEcliptic);
                _SunRtAscen = SolarCal.getSunRtAscen(_SunAppLong, _ObliqCorr);
                _SunDeclin = SolarCal.getSunDeclin(_SunAppLong, _ObliqCorr);
                _varY = SolarCal.getvarY(_ObliqCorr);
                _EqOfTime_min = SolarCal.getEqOfTime_min(_GeomMeanLongSun, _GeomMeanAnomSun, _EccentEarthOrbit, _varY);
                _HASunrise = SolarCal.getHASunrise(Lat, _SunDeclin);
                _SolarNoon = SolarCal.getSolarNoon(Lng, TimeZone, EqOfTime_min);
                _SunriseTime = SolarCal.getSunriseTime(_HASunrise, _SolarNoon);
                _SunsetTime = SolarCal.getSunsetTime(_HASunrise, _SolarNoon);
                _SunlightDuration_min = SolarCal.getSunlightDuration_min(_HASunrise);
                _TrueSolarTime_min = SolarCal.getTrueSolarTime_min(Lng, TimeZone, TimePassMidnight, _EqOfTime_min);
                _HourAngle = SolarCal.getHourAngle(_TrueSolarTime_min);
                _SolarZenithAngle = SolarCal.getSolarZenithAngle(Lat, _SunDeclin, _HourAngle);
                _SolarElevationAngle = SolarCal.getSolarElevationAngle(_SolarZenithAngle);
                _ApproxAtmosphericRefraction = SolarCal.getApproxAtmosphericRefraction(_SolarElevationAngle);
                _SolarElevationCorrectedForAtmRefraction = SolarCal.getSolarElevationCorrectedForAtmRefraction(_SolarElevationAngle, _ApproxAtmosphericRefraction);
                _SolarAzimuthAngle = SolarCal.getSolarAzimuthAngle(Lat, _SunDeclin, _HourAngle, _SolarZenithAngle);
            }

            public SolarCal(int day,int month, int year, double TimePassMidnight, double Lat, double Lng, Int16 TimeZone)
            {

                _JulianDay = SolarCal.getJulianDay(day, month, year, TimeZone, TimePassMidnight);
                _JulianCentury = SolarCal.getJulianCentury(_JulianDay);
                _GeomMeanLongSun = SolarCal.getGeomMeanLongSun(_JulianCentury);
                _GeomMeanAnomSun = SolarCal.getGeomMeanAnomSun(_JulianCentury);
                _EccentEarthOrbit = SolarCal.getEccentEarthOrbit(_JulianCentury);
                _SunEqofCtr = SolarCal.getSunEqofCtr(_JulianCentury, _GeomMeanAnomSun);
                _SunTrueLong = SolarCal.getSunTrueLong(_GeomMeanLongSun, _SunEqofCtr);
                _SunTrueAnom = SolarCal.getSunTrueAnom(_GeomMeanAnomSun, _SunEqofCtr);
                _SunRadVector = SolarCal.getSunRadVector(_EccentEarthOrbit, _SunTrueAnom);
                _SunAppLong = SolarCal.getSunAppLong(_JulianCentury, _SunTrueLong);
                _MeanObliqEcliptic = SolarCal.getMeanObliqEcliptic(_JulianCentury);
                _ObliqCorr = SolarCal.getObliqCorr(_JulianCentury, _MeanObliqEcliptic);
                _SunRtAscen = SolarCal.getSunRtAscen(_SunAppLong, _ObliqCorr);
                _SunDeclin = SolarCal.getSunDeclin(_SunAppLong, _ObliqCorr);
                _varY = SolarCal.getvarY(_ObliqCorr);
                _EqOfTime_min = SolarCal.getEqOfTime_min(_GeomMeanLongSun, _GeomMeanAnomSun, _EccentEarthOrbit, _varY);
                _HASunrise = SolarCal.getHASunrise(Lat, _SunDeclin);
                _SolarNoon = SolarCal.getSolarNoon(Lng, TimeZone, EqOfTime_min);
                _SunriseTime = SolarCal.getSunriseTime(_HASunrise, _SolarNoon);
                _SunsetTime = SolarCal.getSunsetTime(_HASunrise, _SolarNoon);
                _SunlightDuration_min = SolarCal.getSunlightDuration_min(_HASunrise);
                _TrueSolarTime_min = SolarCal.getTrueSolarTime_min(Lng, TimeZone, TimePassMidnight, _EqOfTime_min);
                _HourAngle = SolarCal.getHourAngle(_TrueSolarTime_min);
                _SolarZenithAngle = SolarCal.getSolarZenithAngle(Lat, _SunDeclin, _HourAngle);
                _SolarElevationAngle = SolarCal.getSolarElevationAngle(_SolarZenithAngle);
                _ApproxAtmosphericRefraction = SolarCal.getApproxAtmosphericRefraction(_SolarElevationAngle);
                _SolarElevationCorrectedForAtmRefraction = SolarCal.getSolarElevationCorrectedForAtmRefraction(_SolarElevationAngle, _ApproxAtmosphericRefraction);
                _SolarAzimuthAngle = SolarCal.getSolarAzimuthAngle(Lat, _SunDeclin, _HourAngle, _SolarZenithAngle);
            }


    }
//-----------------------------------------------------------------------------------
    class kUtility
    {

        internal static double myMOD(double a, int b)
        {
            return a - (b * Convert.ToInt32 (a / b));
        }

        internal static double Radians(double ang)
        {
            return Math.PI * ang / 180;
        }
        internal static double Degrees(double Radians)
        {
            return Radians / Math.PI * 180;
        }
        

    }
}
