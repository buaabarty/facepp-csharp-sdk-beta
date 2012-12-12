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
        public Point eye_left { get; set; }
        public Point center { get; set; }
        public double width { get; set; }
        public Attribute attribute { get; set; }
        public Point mouth_left { get; set; }
        public double height { get; set; }
        public Point mouth_right { get; set; }
        public string face_id { get; set; }
        public Point eye_right { get; set; }
    }
    public class DetectResult
    {
        public string url{ get; set; }
        public string img_id{ get; set; }
        public int img_width { get; set; }
        public string session_id { get; set; }
        public IList<Face> face { get; set; }
        public int img_height { get; set; }
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
        public string person_id { get; set; }
        public double confidence { get; set; }
        public string person_name { get; set; }
    }
    public class FaceItemOfRecognize
    {
        public Position position { get; set; }
        public string face_id { get; set; }
        public IList<CandidateItemOfRecognize> candidate { get; set; }
    }
    public class RecognizeResult
    {
        public bool has_untrained_person { get; set; }
        public string session_id { get; set; }
        public IList<FaceItemOfRecognize> face { get; set; }
    }
    public class CandidateItemOfSearch
    {
        public string face_id { get; set; }
        public string similarity { get; set; }
    }
    public class SearchResult
    {
        public bool has_untrained_face { get; set; }
        public string session_id { get; set; }
        public IList<CandidateItemOfSearch> candidate { get; set; }
    }
    public class ManageResult
    {
        public bool success { get; set; }
    }
    public class PersonBasicInfo
    {
        public string person_id { get; set; }
        public string tag { get; set; }
        public string person_name { get; set; }
    }
    public class GroupBasicInfo
    {
        public string group_id { get;set; }
        public string tag { get; set; }
        public string group_name { get; set; }
    }
    public class PersonInfo
    {
        public string person_id { get; set; }
        public string person_name { get; set; }
        public IList<string> face_id { get; set; }
        public IList<GroupBasicInfo> group { get; set; }
    }
    public class GroupInfo
    {
        public IList<PersonBasicInfo> person { get; set; }
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
        public RecognizeResult result { get; set; }
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
}
