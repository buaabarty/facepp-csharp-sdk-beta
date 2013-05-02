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
	
	    public static  String URL_DETECT = "https://apicn.faceplusplus.com/v2/detection/detect"; 
	    public static  String URL_COMPARE = "https://apicn.faceplusplus.com/v2/recognition/compare"; 
	    public static  String URL_RECOGNIZE = "https://apicn.faceplusplus.com/v2/recognition/recognize"; 
	    public static  String URL_SEARCH = "https://apicn.faceplusplus.com/v2/recognition/search"; 
	    public static  String URL_TRAIN = "https://apicn.faceplusplus.com/v2/recognition/train"; 
	    public static  String URL_VERIFY = "https://apicn.faceplusplus.com/v2/recognition/verify"; 
	
	    public static  String URL_PERSON_ADDFACE = "https://apicn.faceplusplus.com/v2/person/add_face";
	    public static  String URL_PERSON_CREATE = "https://apicn.faceplusplus.com/v2/person/create";
	    public static  String URL_PERSON_DELETE = "https://apicn.faceplusplus.com/v2/person/delete";
	    public static  String URL_PERSON_GETINFO = "https://apicn.faceplusplus.com/v2/person/get_info";
	    public static  String URL_PERSON_REMOVEFACE = "https://apicn.faceplusplus.com/v2/person/remove_face";
	    public static  String URL_PERSON_SETINFO = "https://apicn.faceplusplus.com/v2/person/set_info";
	
	    public static  String URL_GROUP_ADDPERSON = "https://apicn.faceplusplus.com/v2/group/add_person";
	    public static  String URL_GROUP_CREATE = "https://apicn.faceplusplus.com/v2/group/create";
	    public static  String URL_GROUP_DELETE = "https://apicn.faceplusplus.com/v2/group/delete";
	    public static  String URL_GROUP_GETINFO = "https://apicn.faceplusplus.com/v2/group/get_info";
	    public static  String URL_GROUP_REMOVEPERSON = "https://apicn.faceplusplus.com/v2/group/remove_person";
	    public static  String URL_GROUP_SETINFO = "https://apicn.faceplusplus.com/v2/group/set_info";

        public static String URL_INFO_GETAPP = "https://apicn.faceplusplus.com/v2/info/get_app";
        public static String URL_INFO_GETFACE = "https://apicn.faceplusplus.com/v2/info/get_face";
        public static String URL_INFO_GETGROUPLIST = "https://apicn.faceplusplus.com/v2/info/get_group_list";
        public static String URL_INFO_GETIMAGE = "https://apicn.faceplusplus.com/v2/info/get_image";
        public static String URL_INFO_GETPERSONLIST = "https://apicn.faceplusplus.com/v2/info/get_person_list";
        public static String URL_INFO_GETQUOTA = "https://apicn.faceplusplus.com/v2/info/get_quota";
        public static String URL_INFO_GETSESSION = "https://apicn.faceplusplus.com/v2/info/get_session";
        public static String URL_INFO_GET_FACESETLIST = "https://apicn.faceplusplus.com/v2/info/get_faceset_list";

        public static String URL_FACESET_CREATE = "https://apicn.faceplusplus.com/v2/faceset/create";
        public static String URL_FACESET_DELETE = "https://apicn.faceplusplus.com/v2/faceset/delete";
        public static String URL_FACESET_ADDFACE = "https://apicn.faceplusplus.com/v2/faceset/add_face";
        public static String URL_FACESET_REMOVEFACE = "https://apicn.faceplusplus.com/v2/faceset/remove_face";
        public static String URL_FACESET_SETINFO = "https://apicn.faceplusplus.com/v2/faceset/set_info";
        public static String URL_FACESET_GET_INFO = "https://apicn.faceplusplus.com/v2/faceset/get_info";

        public static String URL_TRAIN_VERIFY = "https://apicn.faceplusplus.com/v2/train/verify";
        public static String URL_TRAIN_SEARCH = "https://apicn.faceplusplus.com/v2/train/search";
        public static String URL_TRAIN_IDENTIFY = "https://apicn.faceplusplus.com/v2/train/identify";

        public static String URL_GROUPING_GROUPING = "https://apicn.faceplusplus.com/v2/grouping/grouping";
    }
}
