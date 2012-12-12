using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FaceppSDK
{
    class Constants
    {
        public static String FACE_KEY = "2affcadaeddd18f422375adc869f3991";
        public static String FACE_SECRET = "EsU9hmgweuz8U-nwv6s4JP-9AJt64vhz";
	
	    public static  String URL_DETECT = "http://api.faceplusplus.com/detection/detect"; 
	    public static  String URL_COMPARE = "http://api.faceplusplus.com/recognition/compare"; 
	    public static  String URL_RECOGNIZE = "http://api.faceplusplus.com/recognition/recognize"; 
	    public static  String URL_SEARCH = "http://api.faceplusplus.com/recognition/search"; 
	    public static  String URL_TRAIN = "http://api.faceplusplus.com/recognition/train"; 
	    public static  String URL_VERIFY = "http://api.faceplusplus.com/recognition/verify"; 
	
	    public static  String URL_PERSON_ADDFACE = "http://api.faceplusplus.com/person/add_face";
	    public static  String URL_PERSON_CREATE = "http://api.faceplusplus.com/person/create";
	    public static  String URL_PERSON_DELETE = "http://api.faceplusplus.com/person/delete";
	    public static  String URL_PERSON_GETINFO = "http://api.faceplusplus.com/person/get_info";
	    public static  String URL_PERSON_REMOVEFACE = "http://api.faceplusplus.com/person/remove_face";
	    public static  String URL_PERSON_SETINFO = "http://api.faceplusplus.com/person/set_info";
	
	    public static  String URL_GROUP_ADDPERSON = "http://api.faceplusplus.com/group/add_person";
	    public static  String URL_GROUP_CREATE = "http://api.faceplusplus.com/group/create";
	    public static  String URL_GROUP_DELETE = "http://api.faceplusplus.com/group/delete";
	    public static  String URL_GROUP_GETINFO = "http://api.faceplusplus.com/group/get_info";
	    public static  String URL_GROUP_REMOVEPERSON = "http://api.faceplusplus.com/group/remove_person";
	    public static  String URL_GROUP_SETINFO = "http://api.faceplusplus.com/group/set_info";
	
	    public static  String URL_INFO_GETAPP = "http://api.faceplusplus.com/info/get_app";
	    public static  String URL_INFO_GETFACE = "http://api.faceplusplus.com/info/get_face";
	    public static  String URL_INFO_GETGROUPLIST = "http://api.faceplusplus.com/info/get_group_list";
	    public static  String URL_INFO_GETIMAGE = "http://api.faceplusplus.com/info/get_image";
	    public static  String URL_INFO_GETPERSONLIST = "http://api.faceplusplus.com/info/get_person_list";
	    public static  String URL_INFO_GETQUOTA = "http://api.faceplusplus.com/info/get_quota";
	    public static  String URL_INFO_GETSESSION = "http://api.faceplusplus.com/info/get_session";
	
    }
}
