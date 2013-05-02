using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FaceppSDK
{
    /// <summary>
    /// 坐标
    /// </summary>
    public class Point
    {
        public double x{ get; set; }
        public double y{ get; set; }
    }
    /// <summary>
    /// 性别
    /// </summary>
    public class Gender
    {
        public double confidence { get; set; }
        public string value { get; set; }
    }
    /// <summary>
    /// 年龄范围
    /// </summary>
    public class Age 
    {
        public int range { get; set; }
        public int value { get; set; }
    }
    /// <summary>
    /// 种族
    /// </summary>
    public class Race
    {
        public double confidence { get; set; }
        public string value { get; set; }
    }
    /// <summary>
    /// Attribute
    /// </summary>
    public class Attribute
    {
        public Gender gender { get; set; }
        public Age age { get; set; }
        public Race race { get; set; }
    }
    /// <summary>
    /// Detect的Face数据
    /// </summary>
    public class Face
    {
        public Attribute attribute { get; set; }
        public string face_id { get; set; }
        public Position position { get; set; }
        public string tag { get; set; }
    }
    public class DetectResult
    {
        public IList<Face> face { get; set; }
        public int img_height { get; set; }
        public string img_id { get; set; }
        public int img_width { get; set; }
        public string session_id { get; set; }
        public string url{ get; set; }
    }
    public class Component_Similarity
    {
        public double mouth { get; set; }
        public double eye { get; set; }
        public double nose { get; set; }
        public double eyebrow { get; set; }
    }
    public class CompareResult
    {
        public Component_Similarity component_similarity { get; set; }
        public string session_id { get; set; }
        public double similarity { get; set; }
    }
    public class AsyncResult
    {
        public string session_id { get; set; }
    }
    public class TrainResult
    {
        public string session_id { get; set; }
    }
    public class VerifyResult
    {
        public double confidence { get; set; }
        public bool is_same_person { get; set; }
        public string session_id { get; set; }
    }
    public class CandidateItemOfRecognize
    {
        public double confidence { get; set; }
        public string person_id { get; set; }
        public string person_name { get; set; }
        public string tag { get; set; }
    }
    public class FaceItemOfRecognize
    {
        public Position position { get; set; }
        public string face_id { get; set; }
        public IList<CandidateItemOfRecognize> candidate { get; set; }
    }
    public class IdentifyResult
    {
        public string session_id { get; set; }
        public IList<FaceItemOfRecognize> face { get; set; }
    }
    public class CandidateItemOfSearch
    {
        public string face_id { get; set; }
        public string similarity { get; set; }
        public string tag { get; set; }
    }
    public class SearchResult
    {
        public IList<CandidateItemOfSearch> candidate { get; set; }
        public string session_id { get; set; }
    }
    public class ManageResult
    {
        public int added { get; set; }
        public bool success { get; set; }
    }
    public class PersonBasicInfo
    {
        public int added_face { get; set; }
        public int added_group { get; set; }
        public string person_id { get; set; }
        public string person_name { get; set; }
        public string tag { get; set; }
    }
    public class PersonBasicInfoNoNum
    {
        public string person_id { get; set; }
        public string person_name { get; set; }
        public string tag { get; set; }
    }
    public class GroupBasicInfo
    {
        public int added_person { get; set; }
        public string group_id { get;set; }
        public string group_name { get; set; }
        public string tag { get; set; }
    }
    public class GroupBasicInfoNoNum
    {
        public string group_id { get; set; }
        public string group_name { get; set; }
        public string tag { get; set; }
    }
    public class PersonInfo
    {
        public string person_id { get; set; }
        public string person_name { get; set; }
        public IList<string> face_id { get; set; }
        public string tag { get; set; }
        public IList<GroupBasicInfoNoNum> group { get; set; }
    }
    public class GroupInfo
    {
        public IList<PersonBasicInfoNoNum> person { get; set; }
        public string group_id { get; set; }
        public string tag { get; set; }
        public string group_name { get; set; }
    }
    public class AppInfo
    {
        public string description { get; set; }
        public string name { get; set; }
    }
    public class Position
    {
        public Point eye_left { get; set; }
        public Point center { get; set; }
        public double width { get; set; }
        public Point mouth_left { get; set; }
        public double height { get; set; }
        public Point mouth_right { get; set; }
        public Point eye_right { get; set; }
        public Point nose { get; set; }
    }
    public class FaceInfoItem
    {
        public string person_name { get; set; }
        public string url { get; set; }
        public string img_id { get; set; }
        public string tag { get; set; }
        public string person_id { get; set; }
        public Position position { get; set; }
        public string face_id { get; set; }
    }
    public class FaceInfoList
    {
        public IList<FaceInfoItem> face_info { get; set; }
    }
    public class GroupInfoList
    {
        public IList<GroupBasicInfo> group { get; set; }
    }
    public class FaceItemOfImgInfo
    {
        public Position position { get; set; }
        public string face_id { get; set; }
        public string tag { get; set; }
    }
    public class ImgInfo
    {
        public string url { get; set; }
        public string img_id { get; set; }
        public IList<FaceItemOfImgInfo> face { get; set; }
    }
    public class PersonInfoList
    {
        public IList<PersonBasicInfo> person { get; set; }
    }
    public class QuotaInfo
    {
        public int QUOTA_ALL { get; set; }
        public int QUOTA_SEARCH { get; set; }
    }
    public class DetectSessionInfo
    {
        public string status { get; set; }
        public DetectResult result { get; set; }
        public string session_id { get; set; }
    }
    public class CompareSessionInfo
    {
        public string status { get; set; }
        public CompareResult result { get; set; }
        public string session_id { get; set; }
    }
    public class RecognizeSessionInfo
    {
        public string status { get; set; }
        public IdentifyResult result { get; set; }
        public string session_id { get; set; }
    }
    public class SearchSessionInfo
    {
        public string status { get; set; }
        public SearchResult result { get; set; }
        public string session_id { get; set; }
    }
    public class TrainSessionInfo
    {
        public string status { get; set; }
        public TrainResult result { get; set; }
        public string session_id { get; set; }
    }
    public class VerifySessionInfo
    {
        public string status { get; set; }
        public VerifyResult result { get; set; }
        public string session_id { get; set; }
    }
    public class FaceSetCreateResult
    {
        public string added_face { get; set; }
        public string faceset_id { get; set; }
        public string faceset_name { get; set; }
        public string tag { get; set; }
    }
    public class FaceSetDeleteResult
    {
        public int deleted { get; set; }
        public bool success { get; set; }
    }
    public class FaceSetAddResult
    {
        public int added { get; set; }
        public bool success { get; set; }
    }
    public class FaceSetRemoveResult
    {
        public int removed { get; set; }
        public bool success { get; set; }
    }
    public class FaceSetSetInfoResult
    {
        public string faceset_id { get; set; }
        public string faceset_name { get; set; }
        public string tag { get; set; }
    }
    public class FaceBasicInfo
    {
        public string face_id { get; set; }
        public string tag { get; set; }
    }
    public class FaceSetInfoResult
    {
        public IList<FaceBasicInfo> face { get; set; }
        public string faceset_id { get; set; }
        public string faceset_name { get; set; }
        public string tag { get; set; }
    }
    public class FaceSetListInfo
    {
        public IList<FaceBasicInfo> faceset { get; set; }
    }
    public class FaceGroupResultItem
    {
        public FaceBasicInfo[][] group { get; set; }
        public FaceBasicInfo[] ungrouped { get; set; }
    }
    public class GroupingResult
    {
        public int create_time { get; set; }
        public int finish_time { get; set; }
        public FaceGroupResultItem result { get; set; }
        public string session_id { get; set; }
        public string status { get; set; }
    }
}
