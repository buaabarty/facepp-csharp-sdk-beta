using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using log4net;
using System.IO;


namespace FaceppSDK
{
    /// <summary>
    /// Faceplusplus C# SDK
    /// http://faceplusplus.com
    /// </summary>
    public class FaceService : ISerive
    {
        #region 成员列表
        private ILog logger = LogManager.GetLogger(typeof(FaceService));
        private string app_key;
        private string app_secret;
        public IHttpMethod HttpMethod { get; private set; }
        #endregion
        #region 构造函数
        /// <summary>
        /// 创建FaceService服务类
        /// </summary>
        /// <param name="app_key">API Key</param>
        /// <param name="app_secret">API Secret</param>
        /// <returns></returns>
        public FaceService(string _app_key, string _app_secret)
        {
            app_key = _app_key;
            app_secret = _app_secret;
            HttpMethod = new HttpMethods();
        }
        /// <summary>
        /// 创建FaceService服务类
        /// </summary>
        public FaceService()
        {
        }
        #endregion
        #region detection
        /// <summary>
        /// 同步检测给定图片(Image)中的所有人脸(Face)的位置和相应的面部属性
        /// 目前面部属性包括性别(gender), 年龄(age)和种族(race)
        /// 若结果的face_id没有被加入任何faceset/person之中，则在72小时之后过期被自动清除
        /// </summary>
        /// <param name="url">待检测图片的URL</param>
        /// <param name="mode">检测模式可以是normal(默认) 或者 oneface 。在oneface模式中，检测器仅找出图片中最大的一张脸。</param>
        /// <param name="attribute">可以是 all(默认) 或者 none或者由逗号分割的属性列表。目前支持的属性包括：gender, age, race</param>
        /// <param name="tag">可以为图片中检测出的每一张Face指定一个不超过255字节的字符串作为tag，tag信息可以通过 /info/get_face 查询</param>
        /// <returns>返回一个DetectResult，详细内容请参考FaceEntity.cs代码</returns>
        public DetectResult Detection_Detect(string url, string mode = "", string attribute = "", string tag = "")
        {
            var dictionary = new Dictionary<object, object>
            {
                {"url", url}
            };
            if (mode != "") dictionary.Add("mode", mode);
            if (attribute != "") dictionary.Add("attribute", attribute);
            if (tag != "") dictionary.Add("tag", tag);
            return HttpGet<DetectResult>(Constants.URL_DETECT, dictionary);
        }
        /// <summary>
        /// 异步检测给定图片(Image)中的所有人脸(Face)的位置和相应的面部属性
        /// 目前面部属性包括性别(gender), 年龄(age)和种族(race)
        /// 若结果的face_id没有被加入任何faceset/person之中，则在72小时之后过期被自动清除
        /// </summary>
        /// <param name="url">待检测图片的URL</param>
        /// <param name="mode">检测模式可以是normal(默认) 或者 oneface 。在oneface模式中，检测器仅找出图片中最大的一张脸。</param>
        /// <param name="attribute">可以是 all(默认) 或者 none或者由逗号分割的属性列表。目前支持的属性包括：gender, age, race</param>
        /// <param name="tag">可以为图片中检测出的每一张Face指定一个不超过255字节的字符串作为tag，tag信息可以通过 /info/get_face 查询</param>
        /// <returns>返回一个DetectAsyncResult，包含一个字符串session_id</returns>
        public AsyncResult Detection_Detect_Async(string url, string mode = "", string attribute = "", string tag = "")
        {
            var dictionary = new Dictionary<object, object>
            {
                {"url", url}
            };
            if (mode != "") dictionary.Add("mode", mode);
            if (attribute != "") dictionary.Add("attribute", attribute);
            if (tag != "") dictionary.Add("tag", tag);
            dictionary.Add("async", "true");
            return HttpGet<AsyncResult>(Constants.URL_DETECT, dictionary);
        }
        /// <summary>
        /// 同步检测给定图片(Image)中的所有人脸(Face)的位置和相应的面部属性
        /// 目前面部属性包括性别(gender), 年龄(age)和种族(race)
        /// 若结果的face_id没有被加入任何faceset/person之中，则在72小时之后过期被自动清除
        /// </summary>
        /// <param name="filepath">待检测图片的文件路径</param>
        /// <param name="mode">检测模式可以是normal(默认) 或者 oneface 。在oneface模式中，检测器仅找出图片中最大的一张脸。</param>
        /// <param name="attribute">可以是 all(默认) 或者 none或者由逗号分割的属性列表。目前支持的属性包括：gender, age, race</param>
        /// <param name="tag">可以为图片中检测出的每一张Face指定一个不超过255字节的字符串作为tag，tag信息可以通过 /info/get_face 查询</param>
        /// <returns>返回一个DetectResult，详细内容请参考FaceEntity.cs代码</returns>
        public DetectResult Detection_DetectImg(string filepath, string mode = "", string attribute = "", string tag = "")
        {
            var dictionary = new Dictionary<object, object> { };
            FileStream fs = new FileStream(filepath, FileMode.Open);
            byte[] byData = new byte[fs.Length];
            fs.Read(byData, 0, byData.Length);
            fs.Close();
            if (mode != "") dictionary.Add("mode", mode);
            if (attribute != "") dictionary.Add("attribute", attribute);
            if (tag != "") dictionary.Add("tag", tag);
            return HttpPost<DetectResult>(Constants.URL_DETECT, dictionary, byData);
        }
        /// <summary>
        /// 同步检测给定图片(Image)中的所有人脸(Face)的位置和相应的面部属性
        /// 目前面部属性包括性别(gender), 年龄(age)和种族(race)
        /// 若结果的face_id没有被加入任何faceset/person之中，则在72小时之后过期被自动清除
        /// </summary>
        /// <param name="filepath">待检测图片的文件路径</param>
        /// <param name="mode">检测模式可以是normal(默认) 或者 oneface 。在oneface模式中，检测器仅找出图片中最大的一张脸。</param>
        /// <param name="attribute">可以是 all(默认) 或者 none或者由逗号分割的属性列表。目前支持的属性包括：gender, age, race</param>
        /// <param name="tag">可以为图片中检测出的每一张Face指定一个不超过255字节的字符串作为tag，tag信息可以通过 /info/get_face 查询</param>
        /// <returns>返回一个DetectAsyncResult，包含一个字符串session_id</returns>
        public AsyncResult Detection_DetectImg_Async(string filepath, string mode = "", string attribute = "", string tag = "")
        {
            var dictionary = new Dictionary<object, object> { };
            FileStream fs = new FileStream(filepath, FileMode.Open);
            byte[] byData = new byte[fs.Length];
            fs.Read(byData, 0, byData.Length);
            fs.Close();
            if (mode != "") dictionary.Add("mode", mode);
            if (attribute != "") dictionary.Add("attribute", attribute);
            if (tag != "") dictionary.Add("tag", tag);
            return HttpPost<AsyncResult>(Constants.URL_DETECT, dictionary, byData);
        }
        #endregion
        #region recognition
        /// <summary>
        /// 计算两个Face的相似性以及五官相似度
        /// </summary>
        /// <param name="face_id1">第一个Face的face_id</param>
        /// <param name="face_id2">第二个Face的face_id</param>
        /// <returns></returns>
        public CompareResult Recognition_Compare(string face_id1, string face_id2)
        {
            var dictionary = new Dictionary<object, object>
            {
                {"face_id1", face_id1},
                {"face_id2", face_id2}
            };
            return HttpGet<CompareResult>(Constants.URL_COMPARE, dictionary);
        }
        /// <summary>
        /// 对于一个待查询的Face列表（或者对于给定的Image中所有的Face），在一个Group中查询最相似的Person。
        /// 返回结果中如果属性 has_untrained_person 为true时，系统建议您应该重新train此Group。
        /// </summary>
        /// <param name="group_id">识别候选人组成的Group的group_id</param>
        /// <param name="url">待识别图片的URL</param>
        /// <param name="key_face_id">开发者也可以指定一个face_id的列表来表明对这些face进行识别。可以设置此参数key_face_id为一个逗号隔开的face_id列表。</param>
        /// <returns></returns>
        public IdentifyResult Recognition_IdentifyUrlById(string group_id, string url = "", string key_face_id = "")
        {
            var dictionary = new Dictionary<object, object>
            {
                {"group_id", group_id}
            };
            if (url != "") dictionary.Add("url", url);
            if (key_face_id != "") dictionary.Add("key_face_id", key_face_id);
            return HttpGet<IdentifyResult>(Constants.URL_RECOGNIZE, dictionary);
        }
        /// <summary>
        /// 对于一个待查询的Face列表（或者对于给定的Image中所有的Face），在一个Group中查询最相似的Person。
        /// 返回结果中如果属性 has_untrained_person 为true时，系统建议您应该重新train此Group。
        /// </summary>
        /// <param name="group_id">识别候选人组成的Group的group_id</param>
        /// <param name="url">待识别图片的URL</param>
        /// <param name="key_face_id">开发者也可以指定一个face_id的列表来表明对这些face进行识别。可以设置此参数key_face_id为一个逗号隔开的face_id列表。</param>
        /// <returns></returns>
        public AsyncResult Recognition_IdentifyUrlById_Async(string group_id, string url = "", string key_face_id = "")
        {
            var dictionary = new Dictionary<object, object>
            {
                {"group_id", group_id}
            };
            if (url != "") dictionary.Add("url", url);
            if (key_face_id != "") dictionary.Add("key_face_id", key_face_id);
            return HttpGet<AsyncResult>(Constants.URL_RECOGNIZE, dictionary);
        }
        /// <summary>
        /// 对于一个待查询的Face列表（或者对于给定的Image中所有的Face），在一个Group中查询最相似的Person。
        /// 返回结果中如果属性 has_untrained_person 为true时，系统建议您应该重新train此Group。
        /// </summary>
        /// <param name="group_name">识别候选人组成的Group的group_name</param>
        /// <param name="url">待识别图片的URL</param>
        /// <param name="key_face_id">开发者也可以指定一个face_id的列表来表明对这些face进行识别。可以设置此参数key_face_id为一个逗号隔开的face_id列表。</param>
        /// <returns></returns>
        public IdentifyResult Recognition_IdentifyUrlByName(string group_name, string url, string key_face_id = "")
        {
            var dictionary = new Dictionary<object, object>
            {
                {"group_name", group_name}
            };
            if (url != "") dictionary.Add("url", url);
            if (key_face_id != "") dictionary.Add("key_face_id", key_face_id);
            return HttpGet<IdentifyResult>(Constants.URL_RECOGNIZE, dictionary);
        }
        /// <summary>
        /// 对于一个待查询的Face列表（或者对于给定的Image中所有的Face），在一个Group中查询最相似的Person。
        /// 返回结果中如果属性 has_untrained_person 为true时，系统建议您应该重新train此Group。
        /// </summary>
        /// <param name="group_name">识别候选人组成的Group的group_name</param>
        /// <param name="url">待识别图片的URL</param>
        /// <param name="key_face_id">开发者也可以指定一个face_id的列表来表明对这些face进行识别。可以设置此参数key_face_id为一个逗号隔开的face_id列表。</param>
        /// <returns></returns>
        public IdentifyResult Recognition_IdentifyUrlByName_Async(string group_name, string url, string key_face_id = "")
        {
            var dictionary = new Dictionary<object, object>
            {
                {"group_name", group_name}
            };
            if (url != "") dictionary.Add("url", url);
            if (key_face_id != "") dictionary.Add("key_face_id", key_face_id);
            return HttpGet<IdentifyResult>(Constants.URL_RECOGNIZE, dictionary);
        }
        /// <summary>
        /// 对于一个待查询的Face列表（或者对于给定的Image中所有的Face），在一个Group中查询最相似的Person。
        /// 返回结果中如果属性 has_untrained_person 为true时，系统建议您应该重新train此Group。
        /// </summary>
        /// <param name="group_id">识别候选人组成的Group的group_id</param>
        /// <param name="filepath">待识别图片的本地路径</param>
        /// <param name="key_face_id">开发者也可以指定一个face_id的列表来表明对这些face进行识别。可以设置此参数key_face_id为一个逗号隔开的face_id列表。</param>
        /// <returns></returns>
        public IdentifyResult Recognition_IdentifyImgById(string group_id, string filepath, string key_face_id = "")
        {
            var dictionary = new Dictionary<object, object>
            {
                {"group_id", group_id}
            };
            if (key_face_id != "") dictionary.Add("key_face_id", key_face_id);
            if (filepath == "") return HttpPost<IdentifyResult>(Constants.URL_RECOGNIZE, dictionary);
            FileStream fs = new FileStream(filepath, FileMode.Open);
            byte[] byData = new byte[fs.Length];
            fs.Read(byData, 0, byData.Length);
            fs.Close();
            return HttpPost<IdentifyResult>(Constants.URL_RECOGNIZE, dictionary, byData);
        }
        /// <summary>
        /// 对于一个待查询的Face列表（或者对于给定的Image中所有的Face），在一个Group中查询最相似的Person。
        /// 返回结果中如果属性 has_untrained_person 为true时，系统建议您应该重新train此Group。
        /// </summary>
        /// <param name="group_id">识别候选人组成的Group的group_id</param>
        /// <param name="filepath">待识别图片的本地路径</param>
        /// <param name="key_face_id">开发者也可以指定一个face_id的列表来表明对这些face进行识别。可以设置此参数key_face_id为一个逗号隔开的face_id列表。</param>
        /// <returns></returns>
        public IdentifyResult Recognition_IdentifyImgById_Async(string group_id, string filepath, string key_face_id = "")
        {
            var dictionary = new Dictionary<object, object>
            {
                {"group_id", group_id}
            };
            if (key_face_id != "") dictionary.Add("key_face_id", key_face_id);
            if (filepath == "") return HttpPost<IdentifyResult>(Constants.URL_RECOGNIZE, dictionary);
            FileStream fs = new FileStream(filepath, FileMode.Open);
            byte[] byData = new byte[fs.Length];
            fs.Read(byData, 0, byData.Length);
            fs.Close();
            return HttpPost<IdentifyResult>(Constants.URL_RECOGNIZE, dictionary, byData);
        }
        /// <summary>
        /// 对于一个待查询的Face列表（或者对于给定的Image中所有的Face），在一个Group中查询最相似的Person。
        /// 返回结果中如果属性 has_untrained_person 为true时，系统建议您应该重新train此Group。
        /// </summary>
        /// <param name="group_name">识别候选人组成的Group的group_name</param>
        /// <param name="filepath">待识别图片的本地路径</param>
        /// <param name="key_face_id">开发者也可以指定一个face_id的列表来表明对这些face进行识别。可以设置此参数key_face_id为一个逗号隔开的face_id列表。</param>
        /// <returns></returns>
        public IdentifyResult Recognition_IdentifyImgByName(string group_name, string filepath, string key_face_id = "")
        {
            var dictionary = new Dictionary<object, object>
            {
                {"group_name", group_name}
            };
            if (key_face_id != "") dictionary.Add("key_face_id", key_face_id);
            if (filepath == "") return HttpPost<IdentifyResult>(Constants.URL_RECOGNIZE, dictionary);
            FileStream fs = new FileStream(filepath, FileMode.Open);
            byte[] byData = new byte[fs.Length];
            fs.Read(byData, 0, byData.Length);
            fs.Close();
            return HttpPost<IdentifyResult>(Constants.URL_RECOGNIZE, dictionary, byData);
        }
        /// <summary>
        /// 对于一个待查询的Face列表（或者对于给定的Image中所有的Face），在一个Group中查询最相似的Person。
        /// 返回结果中如果属性 has_untrained_person 为true时，系统建议您应该重新train此Group。
        /// </summary>
        /// <param name="group_name">识别候选人组成的Group的group_name</param>
        /// <param name="filepath">待识别图片的本地路径</param>
        /// <param name="key_face_id">开发者也可以指定一个face_id的列表来表明对这些face进行识别。可以设置此参数key_face_id为一个逗号隔开的face_id列表。</param>
        /// <returns></returns>
        public IdentifyResult Recognition_IdentifyImgByName_Async(string group_name, string filepath, string key_face_id = "")
        {
            var dictionary = new Dictionary<object, object>
            {
                {"group_name", group_name}
            };
            if (key_face_id != "") dictionary.Add("key_face_id", key_face_id);
            if (filepath == "") return HttpPost<IdentifyResult>(Constants.URL_RECOGNIZE, dictionary);
            FileStream fs = new FileStream(filepath, FileMode.Open);
            byte[] byData = new byte[fs.Length];
            fs.Read(byData, 0, byData.Length);
            fs.Close();
            return HttpPost<IdentifyResult>(Constants.URL_RECOGNIZE, dictionary, byData);
        }
        /// <summary>
        /// 给定一个Face和一个Faceset，在该Faceset内搜索最相似的Face。
        /// 提示：若搜索集合需要包含超过10000张人脸，可以分成多个faceset分别调用search功能再将结果按confidence顺序合并即可。
        /// 注意，当Faceset中的信息被修改之后（增加，删除了Face等），为了保证结果与最新数据一致，Faceset应当被重新train.
        /// 否则调用此API时将使用最后一次train时的数据。
        /// </summary>
        /// <param name="key_face_id">待搜索的Face的face_id</param>
        /// <param name="faceset_id">指定搜索范围为此Faceset</param>
        /// <param name="count">表示一共获取不超过count个搜索结果。默认count=3</param>
        /// <returns></returns>
        public SearchResult Recognition_SearchById(string key_face_id, string faceset_id, int count = 3)
        {
            var dictionary = new Dictionary<object, object>
            {
                {"key_face_id", key_face_id},
                {"faceset_id", faceset_id},
                {"count", count}
            };
            return HttpGet<SearchResult>(Constants.URL_SEARCH, dictionary);
        }
        /// <summary>
        /// 异步计算
        /// 给定一个Face和一个Faceset，在该Faceset内搜索最相似的Face。
        /// 提示：若搜索集合需要包含超过10000张人脸，可以分成多个faceset分别调用search功能再将结果按confidence顺序合并即可。
        /// 注意，当Faceset中的信息被修改之后（增加，删除了Face等），为了保证结果与最新数据一致，Faceset应当被重新train.
        /// 否则调用此API时将使用最后一次train时的数据。
        /// </summary>
        /// <param name="key_face_id">待搜索的Face的face_id</param>
        /// <param name="faceset_id">指定搜索范围为此Faceset</param>
        /// <param name="count">表示一共获取不超过count个搜索结果。默认count=3</param>
        /// <returns></returns>
        public AsyncResult Recognition_SearchById_Async(string key_face_id, string faceset_id, int count = 3)
        {
            var dictionary = new Dictionary<object, object>
            {
                {"key_face_id", key_face_id},
                {"faceset_id", faceset_id},
                {"count", count}
            };
            dictionary.Add("async", "true");
            return HttpGet<AsyncResult>(Constants.URL_SEARCH, dictionary);
        }
        /// <summary>
        /// 给定一个Face和一个Faceset，在该Faceset内搜索最相似的Face。
        /// 提示：若搜索集合需要包含超过10000张人脸，可以分成多个faceset分别调用search功能再将结果按confidence顺序合并即可。
        /// 注意，当Faceset中的信息被修改之后（增加，删除了Face等），为了保证结果与最新数据一致，Faceset应当被重新train.
        /// 否则调用此API时将使用最后一次train时的数据。
        /// </summary>
        /// <param name="key_face_id">待搜索的Face的face_id</param>
        /// <param name="faceset_name">指定搜索范围为此Faceset</param>
        /// <param name="count">表示一共获取不超过count个搜索结果。默认count=3</param>
        /// <returns></returns>
        public SearchResult Recognition_SearchByName(string key_face_id, string faceset_name, int count = 50)
        {
            var dictionary = new Dictionary<object, object>
            {
                {"key_face_id", key_face_id},
                {"faceset_name", faceset_name},
                {"count", count}
            };
            return HttpGet<SearchResult>(Constants.URL_SEARCH, dictionary);
        }
        /// <summary>
        /// 异步计算
        /// 给定一个Face和一个Faceset，在该Faceset内搜索最相似的Face。
        /// 提示：若搜索集合需要包含超过10000张人脸，可以分成多个faceset分别调用search功能再将结果按confidence顺序合并即可。
        /// 注意，当Faceset中的信息被修改之后（增加，删除了Face等），为了保证结果与最新数据一致，Faceset应当被重新train.
        /// 否则调用此API时将使用最后一次train时的数据。
        /// </summary>
        /// <param name="key_face_id">待搜索的Face的face_id</param>
        /// <param name="faceset_name">指定搜索范围为此Faceset</param>
        /// <param name="count">表示一共获取不超过count个搜索结果。默认count=3</param>
        /// <returns></returns>
        public AsyncResult Recognition_SearchByName_Async(string key_face_id, string faceset_name, int count = 50)
        {
            var dictionary = new Dictionary<object, object>
            {
                {"key_face_id", key_face_id},
                {"faceset_name", faceset_name},
                {"count", count}
            };
            return HttpGet<AsyncResult>(Constants.URL_SEARCH, dictionary);
        }
        /// <summary>
        /// 给定一个Face和一个Person，返回是否是同一个人的判断以及置信度。注意这个Person必须已经被训练过（即其所在的一个Group被训练过，参见 /recognition/train）。
        /// </summary>
        /// <param name="face_id">待verify的face_id</param>
        /// <param name="person_id">对应person的person_id</param>
        /// <returns></returns>
        public VerifyResult Recognition_VerifyById(string face_id, string person_id)
        {
            var dictionary = new Dictionary<object, object>
            {
                {"face_id", face_id},
                {"person_id", person_id}
            };
            return HttpGet<VerifyResult>(Constants.URL_VERIFY, dictionary);
        }
        /// <summary>
        /// 给定一个Face和一个Person，返回是否是同一个人的判断以及置信度。注意这个Person必须已经被训练过（即其所在的一个Group被训练过，参见 /recognition/train）。
        /// </summary>
        /// <param name="face_id">待verify的face_id</param>
        /// <param name="person_name">对应的Person的person_name</param>
        /// <returns></returns>
        public VerifyResult Recognition_VerifyByName(string face_id, string person_name)
        {
            var dictionary = new Dictionary<object, object>
            {
                {"face_id", face_id},
                {"person_name", person_name}
            };
            return HttpGet<VerifyResult>(Constants.URL_VERIFY, dictionary);
        }
        #endregion
        #region person
        /// <summary>
        /// 将一组Face加入到一个Person中。注意，一个Face只能被加入到一个Person中。
        /// </summary>
        /// <param name="person_id">相应Person的person_id</param>
        /// <param name="face_id">一组用逗号分隔的face_id,表示将这些Face加入到相应Person中</param>
        /// <returns></returns>
        public ManageResult Person_AddFaceById(string person_id, string face_id)
        {
            var dictionary = new Dictionary<object, object>
            {
                {"person_id", person_id},
                {"face_id", face_id}
            };
            return HttpGet<ManageResult>(Constants.URL_PERSON_ADDFACE, dictionary);
        }
        /// <summary>
        /// 将一组Face加入到一个Person中。注意，一个Face只能被加入到一个Person中。
        /// </summary>
        /// <param name="person_name">相应Person的person_name</param>
        /// <param name="face_id">一组用逗号分隔的face_id,表示将这些Face加入到相应Person中</param>
        /// <returns></returns>
        public ManageResult Person_AddFaceByName(string person_name, string face_id)
        {
            var dictionary = new Dictionary<object, object>
            {
                {"person_name", person_name},
                {"face_id", face_id}
            };
            return HttpGet<ManageResult>(Constants.URL_PERSON_ADDFACE, dictionary);
        }
        /// <summary>
        /// 创建一个Person
        /// </summary>
        /// <param name="person_name">Person的Name信息，必须在App中全局唯一。Name不能包含字符"@" ，且长度不得超过255。Name也可以不指定，此时系统将产生一个随机的name。</param>
        /// <param name="face_id">一组用逗号分隔的face_id, 表示将这些Face加入到该Person中</param>
        /// <param name="tag">Person相关的tag，不需要全局唯一，可以存储任何信息。长度不能超过255。</param>
        /// <param name="group_id">一组用逗号分割的group id列表。如果该参数被指定，该Person被create之后就会被加入到这些组中。</param>
        /// <param name="group_name">一组用逗号分割的group name列表。如果该参数被指定，该Person被create之后就会被加入到这些组中。</param>
        /// <returns></returns>
        public PersonBasicInfo Person_Create(string person_name = "", string face_id = "", string tag = "", string group_id = "", string group_name = "")
        {
            var dictonary = new Dictionary<object, object> { };
            if (person_name != "") dictonary.Add("person_name", person_name);
            if (face_id != "") dictonary.Add("face_id", face_id);
            if (tag != "") dictonary.Add("tag", tag);
            if (group_id != "") dictonary.Add("group_id", group_id);
            if (group_name != "") dictonary.Add("group_name", group_name);
            return HttpGet<PersonBasicInfo>(Constants.URL_PERSON_CREATE, dictonary);
        }
        /// <summary>
        /// 删除一组Person
        /// </summary>
        /// <param name="person_id">用逗号隔开的待删除的Person id列表</param>
        /// <returns></returns>
        public ManageResult Person_DeleteById(string person_id)
        {
            var dictionary = new Dictionary<object, object>
            {
                {"person_id", person_id}
            };
            return HttpGet<ManageResult>(Constants.URL_PERSON_DELETE, dictionary);
        }
        /// <summary>
        /// 删除一组Person
        /// </summary>
        /// <param name="person_name">用逗号隔开的待删除的Person name列表</param>
        /// <returns></returns>
        public ManageResult Person_DeleteByName(string person_name)
        {
            var dictionary = new Dictionary<object, object>
            {
                {"person_name", person_name}
            };
            return HttpGet<ManageResult>(Constants.URL_PERSON_DELETE, dictionary);
        }
        /// <summary>
        /// 获取一个Person的信息, 包括name, id, tag, 相关的face, 以及groups等信息
        /// </summary>
        /// <param name="person_id">相应Person的id</param>
        /// <returns></returns>
        public PersonInfo Person_GetInfoById(string person_id)
        {
            var dictionary = new Dictionary<object, object>
            {
                {"person_id", person_id}
            };
            return HttpGet<PersonInfo>(Constants.URL_PERSON_GETINFO, dictionary);
        }
        /// <summary>
        /// 获取一个Person的信息, 包括name, id, tag, 相关的face, 以及groups等信息
        /// </summary>
        /// <param name="person_name">相应Person的name</param>
        /// <returns></returns>
        public PersonInfo Person_GetInfoByName(string person_name)
        {
            var dictionary = new Dictionary<object, object>
            {
                {"person_name", person_name}
            };
            return HttpGet<PersonInfo>(Constants.URL_PERSON_GETINFO, dictionary);
        }
        /// <summary>
        /// 删除Person中的一个或多个Face
        /// </summary>
        /// <param name="person_id">相应Person的id</param>
        /// <param name="face_id">一组用逗号分隔的face_id列表，表示将这些face从该Person中删除。开发者也可以指定face_id=all, 表示删除该Person所有相关Face。</param>
        /// <returns></returns>
        public ManageResult Person_RemoveFaceById(string person_id, string face_id)
        {
            var dictionary = new Dictionary<object, object>
            {
                {"person_id", person_id},
                {"face_id", face_id}
            };
            return HttpGet<ManageResult>(Constants.URL_PERSON_REMOVEFACE, dictionary);
        }
        /// <summary>
        /// 删除Person中的一个或多个Face
        /// </summary>
        /// <param name="person_name">相应Person的name</param>
        /// <param name="face_id">一组用逗号分隔的face_id列表，表示将这些face从该Person中删除。开发者也可以指定face_id=all, 表示删除该Person所有相关Face。</param>
        /// <returns></returns>
        public ManageResult Person_RemoveFaceByName(string person_name, string face_id)
        {
            var dictionary = new Dictionary<object, object>
            {
                {"person_name", person_name},
                {"face_id", face_id}
            };
            return HttpGet<ManageResult>(Constants.URL_PERSON_REMOVEFACE, dictionary);
        }
        /// <summary>
        /// 设置Person的name和tag
        /// </summary>
        /// <param name="person_id">相应Person的id</param>
        /// <param name="name">新的name</param>
        /// <param name="tag">新的tag</param>
        /// <returns></returns>
        public PersonBasicInfo Person_SetInfoById(string person_id, string name = "", string tag = "")
        {
            var dictionary = new Dictionary<object, object>
            {
                {"person_id", person_id}
            };
            if (name != "") dictionary.Add("name", name);
            if (tag != "") dictionary.Add("tag", tag);
            return HttpGet<PersonBasicInfo>(Constants.URL_PERSON_SETINFO, dictionary);
        }
        /// <summary>
        /// 设置Person的name和tag
        /// </summary>
        /// <param name="person_name">相应Person的name</param>
        /// <param name="name">新的name</param>
        /// <param name="tag">新的tag</param>
        /// <returns></returns>
        public PersonBasicInfo Person_SetInfoByName(string person_name, string name = "", string tag = "")
        {
            var dictionary = new Dictionary<object, object>
            {
                {"person_name", person_name}
            };
            if (name != "") dictionary.Add("name", name);
            if (tag != "") dictionary.Add("tag", tag);
            return HttpGet<PersonBasicInfo>(Constants.URL_PERSON_SETINFO, dictionary);
        }
        #endregion
        #region group
        /// <summary>
        /// 将一组Person加入到一个Group中。注意，一个Person可以被加入到多个Group中。
        /// </summary>
        /// <param name="group_id">相应Group的id</param>
        /// <param name="person_id">一组用逗号分隔的person_id,表示将这些Person加入到相应Group中</param>
        /// <returns></returns>
        public ManageResult Group_AddPersonByGroupIdPersonId(string group_id, string person_id)
        {
            var dictionary = new Dictionary<object, object>
            {
                {"group_id", group_id},
                {"person_id", person_id}
            };
            return HttpGet<ManageResult>(Constants.URL_GROUP_ADDPERSON, dictionary);
        }
        /// <summary>
        /// 将一组Person加入到一个Group中。注意，一个Person可以被加入到多个Group中。
        /// </summary>
        /// <param name="group_id">相应Group的id</param>
        /// <param name="person_name">一组用逗号分隔的person_name,表示将这些Person加入到相应Group中</param>
        /// <returns></returns>
        public ManageResult Group_AddPersonByGroupIdPersonName(string group_id, string person_name)
        {
            var dictionary = new Dictionary<object, object>
            {
                {"group_id", group_id},
                {"person_name", person_name}
            };
            return HttpGet<ManageResult>(Constants.URL_GROUP_ADDPERSON, dictionary);
        }
        /// <summary>
        /// 将一组Person加入到一个Group中。注意，一个Person可以被加入到多个Group中。
        /// </summary>
        /// <param name="group_name">相应Group的name</param>
        /// <param name="person_name">一组用逗号分隔的person_name,表示将这些Person加入到相应Group中</param>
        /// <returns></returns>
        public ManageResult Group_AddPersonByGroupNamePersonName(string group_name, string person_name)
        {
            var dictionary = new Dictionary<object, object>
            {
                {"group_name", group_name},
                {"person_name", person_name}
            };
            return HttpGet<ManageResult>(Constants.URL_GROUP_ADDPERSON, dictionary);
        }
        /// <summary>
        /// 将一组Person加入到一个Group中。注意，一个Person可以被加入到多个Group中。
        /// </summary>
        /// <param name="group_name">相应Group的name</param>
        /// <param name="person_id">一组用逗号分隔的person_name,表示将这些Person加入到相应Group中</param>
        /// <returns></returns>
        public ManageResult Group_AddPersonByGroupNamePersonId(string group_name, string person_id)
        {
            var dictionary = new Dictionary<object, object>
            {
                {"group_name", group_name},
                {"person_id", person_id}
            };
            return HttpGet<ManageResult>(Constants.URL_GROUP_ADDPERSON, dictionary);
        }
        /// <summary>
        /// 将一组Person加入到一个Group中。注意，一个Person可以被加入到多个Group中。
        /// </summary>
        /// <param name="group_id">相应Group的id，不需要则输入空串</param>
        /// <param name="group_name">相应Group的name，不需要则输入空串</param>
        /// <param name="person_id">一组用逗号分隔的person_id,表示将这些Person加入到相应Group中，不需要则输入空串</param>
        /// <param name="person_name">一组用逗号分隔的person_name,表示将这些Person加入到相应Group中，不需要则输入空串</param>
        /// <returns></returns>
        public ManageResult Group_AddPerson(string group_id = "", string group_name = "", string person_id = "", string person_name = "")
        {
            var dictionary = new Dictionary<object, object> { };
            ManageResult res = new ManageResult();
            if ((group_id == "") == (group_name == "") || (person_id == "") == (person_name == ""))
            {
                res.success = false;
            }
            else
            {
                if (group_id != "")
                {
                    if (person_id != "") res = Group_AddPersonByGroupIdPersonId(group_id, person_id);
                    else res = Group_AddPersonByGroupIdPersonName(group_id, person_name);
                }
                else
                {
                    if (person_id != "") res = Group_AddPersonByGroupNamePersonId(group_name, person_id);
                    else res = Group_AddPersonByGroupNamePersonName(group_name, person_name);
                }
            }
            return res;
        }
        /// <summary>
        /// 创建一个Group
        /// </summary>
        /// <param name="group_name">Group的Name信息，必须在App中全局唯一。Name不能包含字符"@" ，且长度不得超过255。</param>
        /// <param name="tag">Group的tag，不需全局唯一，可以存储任何信息。长度不能超过255。</param>
        /// <param name="person_id">一组用逗号分隔的person_id, 表示将这些Person加入到该Group中。注意，一个Person可以被加入到多个Group中。</param>
        /// <returns></returns>
        public GroupBasicInfo Group_CreateByIdList(string group_name, string tag = "", string person_id = "")
        {
            var dictionary = new Dictionary<object, object>
            {
                {"group_name", group_name}
            };
            if (tag != "") dictionary.Add("tag", tag);
            if (person_id != "") dictionary.Add("person_id", person_id);
            return HttpGet<GroupBasicInfo>(Constants.URL_GROUP_CREATE, dictionary);
        }
        /// <summary>
        /// 创建一个Group
        /// </summary>
        /// <param name="group_name">Group的Name信息，必须在App中全局唯一。Name不能包含字符"@" ，且长度不得超过255。</param>
        /// <param name="tag">Group的tag，不需全局唯一，可以存储任何信息。长度不能超过255。</param>
        /// <param name="person_name">一组用逗号分隔的person_name, 表示将这些Person加入到该Group中。注意，一个Person可以被加入到多个Group中。</param>
        /// <returns></returns>
        public GroupBasicInfo Group_CreateByNameList(string group_name, string tag = "", string person_name = "")
        {
            var dictionary = new Dictionary<object, object>
            {
                {"group_name", group_name}
            };
            if (tag != "") dictionary.Add("tag", tag);
            if (person_name != "") dictionary.Add("person_name", person_name);
            return HttpGet<GroupBasicInfo>(Constants.URL_GROUP_CREATE, dictionary);
        }
        /// <summary>
        /// 删除一组Group
        /// </summary>
        /// <param name="group_id">一组用逗号分割的group_id，表示删除这些Group</param>
        /// <returns></returns>
        public ManageResult Group_DeleteById(string group_id)
        {
            var dictionary = new Dictionary<object, object>
            {
                {"group_id", group_id}
            };
            return HttpGet<ManageResult>(Constants.URL_GROUP_DELETE, dictionary);
        }
        /// <summary>
        /// 删除一组Group
        /// </summary>
        /// <param name="group_name">一组用逗号分割的group_name，表示删除这些Group</param>
        /// <returns></returns>
        public ManageResult Group_DeleteByName(string group_name)
        {
            var dictionary = new Dictionary<object, object>
            {
                {"group_name", group_name}
            };
            return HttpGet<ManageResult>(Constants.URL_GROUP_DELETE, dictionary);
        }
        /// <summary>
        /// 获取Group的信息，包括Group中的Person列表，Group的tag等信息
        /// </summary>
        /// <param name="group_id">待查询Group的id。开发者也可以指定group_id=none，此时将返回所有未加入任何Group的Person。</param>
        /// <returns></returns>
        public GroupInfo Group_GetInfoById(string group_id)
        {
            var dictionary = new Dictionary<object, object>
            {
                {"group_id", group_id}
            };
            return HttpGet<GroupInfo>(Constants.URL_GROUP_GETINFO, dictionary);
        }
        /// <summary>
        /// 获取Group的信息，包括Group中的Person列表，Group的tag等信息
        /// </summary>
        /// <param name="group_name">待查询Group的name。</param>
        /// <returns></returns>
        public GroupInfo Group_GetInfoByName(string group_name)
        {
            var dictionary = new Dictionary<object, object>
            {
                {"group_name", group_name}
            };
            return HttpGet<GroupInfo>(Constants.URL_GROUP_GETINFO, dictionary);
        }
        /// <summary>
        /// 从Group中删除一组Person
        /// </summary>
        /// <param name="group_id">相应Group的id</param>
        /// <param name="person_id">一组用逗号分割的person_id列表，表示将这些person从该Group中删除。开发者也可以指定person_id=all, 表示删掉该Group中所有Person。</param>
        /// <returns></returns>
        public ManageResult Group_RemovePersonByGroupIdPersonId(string group_id, string person_id)
        {
            var dictionary = new Dictionary<object, object>
            {
                {"group_id", group_id},
                {"person_id", person_id}
            };
            return HttpGet<ManageResult>(Constants.URL_GROUP_REMOVEPERSON, dictionary);
        }
        /// <summary>
        /// 从Group中删除一组Person
        /// </summary>
        /// <param name="group_id">相应Group的id</param>
        /// <param name="person_name">一组用逗号分割的person_name列表，表示将这些person从该Group中删除。</param>
        /// <returns></returns>
        public ManageResult Group_RemovePersonByGroupIdPersonName(string group_id, string person_name)
        {
            var dictionary = new Dictionary<object, object>
            {
                {"group_id", group_id},
                {"person_name", person_name}
            };
            return HttpGet<ManageResult>(Constants.URL_GROUP_REMOVEPERSON, dictionary);
        }
        /// <summary>
        /// 从Group中删除一组Person
        /// </summary>
        /// <param name="group_name">相应Group的name</param>
        /// <param name="person_name">一组用逗号分割的person_name列表，表示将这些person从该Group中删除。</param>
        /// <returns></returns>
        public ManageResult Group_RemovePersonByGroupNamePersonName(string group_name, string person_name)
        {
            var dictionary = new Dictionary<object, object>
            {
                {"group_name", group_name},
                {"person_name", person_name}
            };
            return HttpGet<ManageResult>(Constants.URL_GROUP_REMOVEPERSON, dictionary);
        }
        /// <summary>
        /// 从Group中删除一组Person
        /// </summary>
        /// <param name="group_name">相应Group的name</param>
        /// <param name="person_id">一组用逗号分割的person_id列表，表示将这些person从该Group中删除。开发者也可以指定person_id=all, 表示删掉该Group中所有Person。</param>
        /// <returns></returns>
        public ManageResult Group_RemovePersonByGroupNamePersonId(string group_name, string person_id)
        {
            var dictionary = new Dictionary<object, object>
            {
                {"group_name", group_name},
                {"person_id", person_id}
            };
            return HttpGet<ManageResult>(Constants.URL_GROUP_REMOVEPERSON, dictionary);
        }
        /// <summary>
        /// 从Group中删除一组Person
        /// </summary>
        /// <param name="group_id">相应Group的id，不需要则填入空串</param>
        /// <param name="group_name">相应Group的name，不需要则填入空串</param>
        /// <param name="person_id">一组用逗号分割的person_id列表，表示将这些person从该Group中删除。开发者也可以指定person_id=all, 表示删掉该Group中所有Person。不需要则填入空串。</param>
        /// <param name="person_name">一组用逗号分割的person_name列表，表示将这些person从该Group中删除。不需要则填入空串。</param>
        /// <returns></returns>
        public ManageResult Group_RemovePerson(string group_id = "", string group_name = "", string person_id = "", string person_name = "")
        {
            var dictionary = new Dictionary<object, object> { };
            ManageResult res = new ManageResult();
            if ((group_id == "") == (group_name == "") || (person_id == "") == (person_name == ""))
            {
                res.success = false;
            }
            else
            {
                if (group_id != "")
                {
                    if (person_id != "") res = Group_RemovePersonByGroupIdPersonId(group_id, person_id);
                    else res = Group_RemovePersonByGroupIdPersonName(group_id, person_name);
                }
                else
                {
                    if (person_id != "") res = Group_RemovePersonByGroupNamePersonId(group_name, person_id);
                    else res = Group_RemovePersonByGroupNamePersonName(group_name, person_name);
                }
            }
            return res;
        }
        /// <summary>
        /// 设置Group的name和tag
        /// </summary>
        /// <param name="group_id">相应Group的id</param>
        /// <param name="name">新的group_name</param>
        /// <param name="tag">新的tag</param>
        /// <returns></returns>
        public GroupBasicInfo Group_SetInfoById(string group_id, string name = "", string tag = "")
        {
            var dictionary = new Dictionary<object, object>
            {
                {"group_id", group_id}
            };
            if (name != "") dictionary.Add("name", name);
            if (tag != "") dictionary.Add("tag", tag);
            return HttpGet<GroupBasicInfo>(Constants.URL_GROUP_SETINFO, dictionary);
        }
        /// <summary>
        /// 设置Group的name和tag
        /// </summary>
        /// <param name="group_name">相应Group的name</param>
        /// <param name="name">新的group_name</param>
        /// <param name="tag">新的tag</param>
        /// <returns></returns>
        public GroupBasicInfo Group_SetInfoByName(string group_name, string name = "", string tag = "")
        {
            var dictionary = new Dictionary<object, object>
            {
                {"group_name", group_name}
            };
            if (name != "") dictionary.Add("name", name);
            if (tag != "") dictionary.Add("tag", tag);
            return HttpGet<GroupBasicInfo>(Constants.URL_GROUP_SETINFO, dictionary);
        }
        #endregion
        #region FaceSet
        /// <summary>
        /// 创建一个Faceset
        /// 一个Faceset最多允许包含10000个Face。
        /// </summary>
        /// <param name="faceset_name">Faceset的Name信息，必须在App中全局唯一。Name不能包含^@,&=*'"等非法字符，且长度不得超过255。Name也可以不指定，此时系统将产生一个随机的name。</param>
        /// <param name="face_id">一组用逗号分隔的face_id, 表示将这些Face加入到该Faceset中</param>
        /// <param name="tag">Faceset相关的tag，不需要全局唯一，不能包含^@,&=*'"等非法字符，长度不能超过255。</param>
        /// <returns></returns>
        public FaceSetCreateResult FaceSet_Create(string faceset_name = "", string face_id = "", string tag = "")
        {
            var dictionary = new Dictionary<object, object>();
            if (faceset_name != "") dictionary.Add("faceset_name", faceset_name);
            if (face_id != "") dictionary.Add("face_id", face_id);
            if (tag != "") dictionary.Add("tag", tag);
            return HttpGet<FaceSetCreateResult>(Constants.URL_FACESET_CREATE, dictionary);
        }
        /// <summary>
        /// 删除一组Faceset
        /// </summary>
        /// <param name="faceset_name">用逗号隔开的待删除的faceset name列表</param>
        /// <returns>返回是否成功删除，以及成功删除的FaceSet个数</returns>
        public FaceSetDeleteResult FaseSet_DeleteByName(string faceset_name)
        {
            var dictionary = new Dictionary<object, object>
            {
                {"faceset_name", faceset_name} 
            };
            return HttpGet<FaceSetDeleteResult>(Constants.URL_FACESET_DELETE, dictionary);
        }
        /// <summary>
        /// 删除一组FaceSet
        /// </summary>
        /// <param name="faceset_id">用逗号隔开的待删除的faceset id列表</param>
        /// <returns>返回是否成功删除，以及成功删除的FaceSet个数</returns>
        public FaceSetDeleteResult FaseSet_DeleteById(string faceset_id)
        {
            var dictionary = new Dictionary<object, object>
            {
                {"faceset_id", faceset_id} 
            };
            return HttpGet<FaceSetDeleteResult>(Constants.URL_FACESET_DELETE, dictionary);
        }
        /// <summary>
        /// 将一组Face加入到一个Faceset中。
        /// 一个Faceset最多允许包含10000个Face。
        /// </summary>
        /// <param name="faceset_name">相应Faceset的name</param>
        /// <param name="face_id">一组用逗号分隔的face_id,表示将这些Face加入到相应Faceset中。</param>
        /// <returns>返回成功加入的face数量和是否操作成功</returns>
        public FaceSetAddResult FaceSet_AddByName(string faceset_name, string face_id)
        {
            var dictionary = new Dictionary<object, object>
            {
                {"faceset_name", faceset_name},
                {"face_id", face_id}
            };
            return HttpGet<FaceSetAddResult>(Constants.URL_FACESET_ADDFACE, dictionary);
        }
        /// <summary>
        /// 将一组Face加入到一个Faceset中。
        /// 一个Faceset最多允许包含10000个Face。
        /// </summary>
        /// <param name="faceset_id">相应Faceset的id</param>
        /// <param name="face_id">一组用逗号分隔的face_id,表示将这些Face加入到相应Faceset中。</param>
        /// <returns>返回成功加入的face数量和是否操作成功</returns>
        public FaceSetAddResult FaceSet_AddById(string faceset_id, string face_id)
        {
            var dictionary = new Dictionary<object, object>
            {
                {"faceset_id", faceset_id},
                {"face_id", face_id}
            };
            return HttpGet<FaceSetAddResult>(Constants.URL_FACESET_ADDFACE, dictionary);
        }
        /// <summary>
        /// 删除Faceset中的一个或多个Face
        /// </summary>
        /// <param name="faceset_id">相应faceset的id</param>
        /// <param name="face_id">一组用逗号分隔的face_id列表，表示将这些face从该faceset中删除。开发者也可以指定face_id=all, 表示删除该faceset所有相关Face。</param>
        /// <returns>返回成功删除的face数量和是否操作成功</returns>
        public FaceSetRemoveResult FaceSet_RemoveFaceById(string faceset_id, string face_id)
        {
            var dictionary = new Dictionary<object, object>
            {
                {"faceset_id", faceset_id},
                {"face_id", face_id}
            };
            return HttpGet<FaceSetRemoveResult>(Constants.URL_FACESET_REMOVEFACE, dictionary);
        }
        /// <summary>
        /// 删除Faceset中的一个或多个Face
        /// </summary>
        /// <param name="faceset_name">相应faceset的name</param>
        /// <param name="face_id">一组用逗号分隔的face_id列表，表示将这些face从该faceset中删除。开发者也可以指定face_id=all, 表示删除该faceset所有相关Face。</param>
        /// <returns>返回成功删除的face数量和是否操作成功</returns>
        public FaceSetRemoveResult FaceSet_RemoveFaceByName(string faceset_name, string face_id)
        {
            var dictionary = new Dictionary<object, object>
            {
                {"faceset_name", faceset_name},
                {"face_id", face_id}
            };
            return HttpGet<FaceSetRemoveResult>(Constants.URL_FACESET_REMOVEFACE, dictionary);
        }
        /// <summary>
        /// 设置faceset的name和tag
        /// </summary>
        /// <param name="faceset_id">相应faceset的id</param>
        /// <param name="name">新的name</param>
        /// <param name="tag">新的tag</param>
        /// <returns></returns>
        public FaceSetSetInfoResult FaceSet_SetInfoById(string faceset_id, string name = "", string tag = "")
        {
            var dictionary = new Dictionary<object, object>
            {
                {"faceset_id", faceset_id}
            };
            if (name != "") dictionary.Add("name", name);
            if (tag != "") dictionary.Add("tag", tag);
            return HttpGet<FaceSetSetInfoResult>(Constants.URL_FACESET_SETINFO, dictionary);
        }
        /// <summary>
        /// 设置faceset的name和tag
        /// </summary>
        /// <param name="faceset_name">相应faceset的name</param>
        /// <param name="name">新的name</param>
        /// <param name="tag">新的tag</param>
        /// <returns></returns>
        public FaceSetSetInfoResult FaceSet_SetInfoByName(string faceset_name, string name = "", string tag = "")
        {
            var dictionary = new Dictionary<object, object>
            {
                {"faceset_name", faceset_name}
            };
            if (name != "") dictionary.Add("name", name);
            if (tag != "") dictionary.Add("tag", tag);
            return HttpGet<FaceSetSetInfoResult>(Constants.URL_FACESET_SETINFO, dictionary);
        }
        /// <summary>
        /// 获取一个faceset的信息, 包括name, id, tag, 以及相关的face等信息
        /// </summary>
        /// <param name="faceset_id">相应faceset的id</param>
        /// <returns></returns>
        public FaceSetInfoResult FaceSet_GetInfoById(string faceset_id)
        {
            var dictionary = new Dictionary<object, object>
            {
                {"faceset_id", faceset_id}
            };
            return HttpGet<FaceSetInfoResult>(Constants.URL_FACESET_GET_INFO, dictionary);
        }
        /// <summary>
        /// 获取一个faceset的信息, 包括name, id, tag, 以及相关的face等信息
        /// </summary>
        /// <param name="faceset_name">相应faceset的name</param>
        /// <returns></returns>
        public FaceSetInfoResult FaceSet_GetInfoByName(string faceset_name)
        {
            var dictionary = new Dictionary<object, object>
            {
                {"faceset_name", faceset_name}
            };
            return HttpGet<FaceSetInfoResult>(Constants.URL_FACESET_GET_INFO, dictionary);
        }
        #endregion
        #region train
        /// <summary>
        /// 针对verify功能对一个person进行训练。请注意:
        ///     在一个person内进行verify之前，必须先对该person进行Train
        ///     当一个person内的数据被修改后(例如增删Person相关的Face等)，为使这些修改生效，person应当被重新Train
        ///     Train所花费的时间较长, 因此该调用是异步的，仅返回session_id。
        ///     训练的结果可以通过/info/get_session查询。当训练完成时，返回值中将包含{"success": true}
        /// </summary>
        /// <param name="person_id">验证对象person</param>
        /// <returns>相应请求的session标识符，可用于结果查询</returns>
        public AsyncResult Train_VerifyById(string person_id)
        {
            var dictionary = new Dictionary<object, object> {
                {"person_id", person_id}
            };
            return HttpGet<AsyncResult>(Constants.URL_TRAIN_VERIFY, dictionary);
        }
        /// <summary>
        /// 针对verify功能对一个person进行训练。请注意:
        ///     在一个person内进行verify之前，必须先对该person进行Train
        ///     当一个person内的数据被修改后(例如增删Person相关的Face等)，为使这些修改生效，person应当被重新Train
        ///     Train所花费的时间较长, 因此该调用是异步的，仅返回session_id。
        ///     训练的结果可以通过/info/get_session查询。当训练完成时，返回值中将包含{"success": true}
        /// </summary>
        /// <param name="person_name">验证对象person</param>
        /// <returns>相应请求的session标识符，可用于结果查询</returns>
        public AsyncResult Train_VerifyByName(string person_name)
        {
            var dictionary = new Dictionary<object, object> {
                {"person_name", person_name}
            };
            return HttpGet<AsyncResult>(Constants.URL_TRAIN_VERIFY, dictionary);
        }
        /// <summary>
        /// 针对search功能对一个faceset进行训练。请注意:
        ///     在一个faceset内进行search之前，必须先对该faceset进行Train
        ///     当一个faceset内的数据被修改后(例如增删Face等)，为使这些修改生效，faceset应当被重新Train
        ///     Train所花费的时间较长, 因此该调用是异步的，仅返回session_id。
        ///     训练的结果可以通过 /info/get_session 查询。当训练完成时，返回值中将包含{"success": true}
        /// </summary>
        /// <param name="faceset_id">用于搜索的face组成的faceset</param>
        /// <returns></returns>
        public AsyncResult Train_SearchById(string faceset_id)
        {
            var dictionary = new Dictionary<object, object> {
                {"faceset_id", faceset_id}
            };
            return HttpGet<AsyncResult>(Constants.URL_TRAIN_SEARCH, dictionary);
        }
        /// <summary>
        /// 针对search功能对一个faceset进行训练。请注意:
        ///     在一个faceset内进行search之前，必须先对该faceset进行Train
        ///     当一个faceset内的数据被修改后(例如增删Face等)，为使这些修改生效，faceset应当被重新Train
        ///     Train所花费的时间较长, 因此该调用是异步的，仅返回session_id。
        ///     训练的结果可以通过 /info/get_session 查询。当训练完成时，返回值中将包含{"success": true}
        /// </summary>
        /// <param name="faceset_name">用于搜索的face组成的faceset</param>
        /// <returns></returns>
        public AsyncResult Train_SearchByName(string faceset_name)
        {
            var dictionary = new Dictionary<object, object> {
                {"faceset_name", faceset_name}
            };
            return HttpGet<AsyncResult>(Constants.URL_TRAIN_SEARCH, dictionary);
        }
        /// <summary>
        /// 针对identify功能对一个Group进行训练。请注意:
        ///     在一个Group内进行identify之前，必须先对该Group进行Train
        ///     当一个Group内的数据被修改后(例如增删Person, 增删Person相关的Face等)，为使这些修改生效，Group应当被重新Train
        ///     Train所花费的时间较长, 因此该调用是异步的，仅返回session_id。
        ///     训练的结果可以通过 /info/get_session 查询。当训练完成时，返回值中将包含{"success": true}
        /// </summary>
        /// <param name="group_id">识别候选人组成的Group</param>
        /// <returns></returns>
        public AsyncResult Train_IdentifyById(string group_id)
        {
            var dictionary = new Dictionary<object, object>
            {
                {"group_id", group_id}
            };
            return HttpGet<AsyncResult>(Constants.URL_TRAIN_IDENTIFY, dictionary);
        }
        /// <summary>
        /// 针对identify功能对一个Group进行训练。请注意:
        ///     在一个Group内进行identify之前，必须先对该Group进行Train
        ///     当一个Group内的数据被修改后(例如增删Person, 增删Person相关的Face等)，为使这些修改生效，Group应当被重新Train
        ///     Train所花费的时间较长, 因此该调用是异步的，仅返回session_id。
        ///     训练的结果可以通过 /info/get_session 查询。当训练完成时，返回值中将包含{"success": true}
        /// </summary>
        /// <param name="group_name">识别候选人组成的Group</param>
        /// <returns></returns>
        public AsyncResult Train_IdentifyByName(string group_name)
        {
            var dictionary = new Dictionary<object, object>
            {
                {"group_name", group_name}
            };
            return HttpGet<AsyncResult>(Constants.URL_TRAIN_IDENTIFY, dictionary);
        }
        #endregion
        #region grouping
        /// <summary>
        /// 给出一个Faceset，尝试将其分类，使得来自同一个人的Face被放在同一类中。
        /// Grouping所花费的时间较长, 因此该调用是异步的，仅返回session_id。
        /// </summary>
        /// <param name="faceset_id">相应Faceset的id</param>
        /// <returns></returns>
        public AsyncResult Grouping_GroupingById(string faceset_id)
        {
            var dictionary = new Dictionary<object, object>
            {
                {"faceset_id", faceset_id}
            };
            return HttpGet<AsyncResult>(Constants.URL_GROUPING_GROUPING, dictionary);
        }
        /// <summary>
        /// 给出一个Faceset，尝试将其分类，使得来自同一个人的Face被放在同一类中。
        /// Grouping所花费的时间较长, 因此该调用是异步的，仅返回session_id。
        /// </summary>
        /// <param name="faceset_name">相应Faceset的name</param>
        /// <returns></returns>
        public AsyncResult Grouping_GroupingByName(string faceset_name)
        {
            var dictionary = new Dictionary<object, object>
            {
                {"faceset_name", faceset_name}
            };
            return HttpGet<AsyncResult>(Constants.URL_GROUPING_GROUPING, dictionary);
        }
        #endregion
        #region info
        /// <summary>
        /// 获取该App相关的信息
        /// </summary>
        /// <returns></returns>
        public AppInfo Info_GetApp()
        {
            var dictionary = new Dictionary<object, object> { };
            return HttpGet<AppInfo>(Constants.URL_INFO_GETAPP, dictionary);
        }
        /// <summary>
        /// 给定一组Face，返回相应的信息(包括源图片, 相关的person等等)
        /// </summary>
        /// <param name="face_id">一组用逗号分割的face_id</param>
        /// <returns></returns>
        public FaceInfoList Info_GetFace(string face_id)
        {
            var dictionary = new Dictionary<object, object>
            {
                {"face_id", face_id}
            };
            return HttpGet<FaceInfoList>(Constants.URL_INFO_GETFACE, dictionary);
        }
        /// <summary>
        /// 返回这个App中的所有Group
        /// </summary>
        /// <returns></returns>
        public GroupInfoList Info_GetGroupList()
        {
            var dictionary = new Dictionary<object, object> { };
            return HttpGet<GroupInfoList>(Constants.URL_INFO_GETGROUPLIST, dictionary);
        }
        /// <summary>
        /// 获取一张image的信息, 包括其中包含的face等信息
        /// </summary>
        /// <param name="img_id">目标图片的img_id</param>
        /// <returns></returns>
        public ImgInfo Info_GetImage(string img_id)
        {
            var dictionary = new Dictionary<object, object>
            {
                {"img_id", img_id}
            };
            return HttpGet<ImgInfo>(Constants.URL_INFO_GETIMAGE, dictionary);
        }
        /// <summary>
        /// 返回该App中的所有Person
        /// </summary>
        /// <returns></returns>
        public PersonInfoList Info_GetPersonList()
        {
            var dictionary = new Dictionary<object, object> { };
            return HttpGet<PersonInfoList>(Constants.URL_INFO_GETPERSONLIST, dictionary);
        }
        /// <summary>
        /// 返回剩余Quota
        /// </summary>
        /// <returns></returns>
        public QuotaInfo Info_GetQuota()
        {
            var dictionary = new Dictionary<object, object> { };
            return HttpGet<QuotaInfo>(Constants.URL_INFO_GETQUOTA, dictionary);
        }
        /// <summary>
        /// 获取session相关状态和结果
        /// 可能的status：INQUEUE(队列中), SUCC(成功), EXPIRED(session 超时) 和FAILED(失败)
        /// 当status是SUCC时，返回结果中还包含session对应的结果
        /// </summary>
        /// <param name="session_id">由/detection中的API调用产生的session_id</param>
        /// <returns></returns>
        public DetectSessionInfo Info_GetDetectSession(string session_id)
        {
            var dictionary = new Dictionary<object, object>
            {
                {"session_id", session_id}
            };
            return HttpGet<DetectSessionInfo>(Constants.URL_INFO_GETSESSION, dictionary);
        }
        /// <summary>
        /// 获取session相关状态和结果
        /// 可能的status：INQUEUE(队列中), SUCC(成功), EXPIRED(session 超时) 和FAILED(失败)
        /// 当status是SUCC时，返回结果中还包含session对应的结果
        /// </summary>
        /// <param name="session_id">由/recognition/compare中的API调用产生的session_id</param>
        /// <returns></returns>
        public CompareSessionInfo Info_GetCompareSession(string session_id)
        {
            var dictionary = new Dictionary<object, object>
            {
                {"session_id", session_id}
            };
            return HttpGet<CompareSessionInfo>(Constants.URL_INFO_GETSESSION, dictionary);
        }
        /// <summary>
        /// 获取session相关状态和结果
        /// 可能的status：INQUEUE(队列中), SUCC(成功), EXPIRED(session 超时) 和FAILED(失败)
        /// 当status是SUCC时，返回结果中还包含session对应的结果
        /// </summary>
        /// <param name="session_id">由/recognition/recognize中的API调用产生的session_id</param>
        /// <returns></returns>
        public RecognizeSessionInfo Info_GetRecognizeSession(string session_id)
        {
            var dictionary = new Dictionary<object, object>
            {
                {"session_id", session_id}
            };
            return HttpGet<RecognizeSessionInfo>(Constants.URL_INFO_GETSESSION, dictionary);
        }
        /// <summary>
        /// 获取session相关状态和结果
        /// 可能的status：INQUEUE(队列中), SUCC(成功), EXPIRED(session 超时) 和FAILED(失败)
        /// 当status是SUCC时，返回结果中还包含session对应的结果
        /// </summary>
        /// <param name="session_id">由/recognition/search中的API调用产生的session_id</param>
        /// <returns></returns>
        public SearchSessionInfo Info_GetSearchSession(string session_id)
        {
            var dictionary = new Dictionary<object, object>
            {
                {"session_id", session_id}
            };
            return HttpGet<SearchSessionInfo>(Constants.URL_INFO_GETSESSION, dictionary);
        }
        /// <summary>
        /// 获取session相关状态和结果
        /// 可能的status：INQUEUE(队列中), SUCC(成功), EXPIRED(session 超时) 和FAILED(失败)
        /// 当status是SUCC时，返回结果中还包含session对应的结果
        /// </summary>
        /// <param name="session_id">由/recognition/train中的API调用产生的session_id</param>
        /// <returns></returns>
        public TrainSessionInfo Info_GetTrainSession(string session_id)
        {
            var dictionary = new Dictionary<object, object>
            {
                {"session_id", session_id}
            };
            return HttpGet<TrainSessionInfo>(Constants.URL_INFO_GETSESSION, dictionary);
        }
        /// <summary>
        /// 获取session相关状态和结果
        /// 可能的status：INQUEUE(队列中), SUCC(成功), EXPIRED(session 超时) 和FAILED(失败)
        /// 当status是SUCC时，返回结果中还包含session对应的结果
        /// </summary>
        /// <param name="session_id">由/recognition/verify中的API调用产生的session_id</param>
        /// <returns></returns>
        public VerifySessionInfo Info_GetVerifySession(string session_id)
        {
            var dictionary = new Dictionary<object, object>
            {
                {"session_id", session_id}
            };
            return HttpGet<VerifySessionInfo>(Constants.URL_INFO_GETSESSION, dictionary);
        }
        /// <summary>
        /// 获取session相关状态和结果
        /// 可能的status：INQUEUE(队列中), SUCC(成功), EXPIRED(session 超时) 和FAILED(失败)
        /// 当status是SUCC时，返回结果中还包含session对应的结果
        /// </summary>
        /// <param name="session_id"></param>
        /// <returns></returns>
        public GroupingResult Info_GetGroupingSession(string session_id)
        {
            var dictionary = new Dictionary<object, object>
            {
                {"session_id", session_id}
            };
            return HttpGet<GroupingResult>(Constants.URL_INFO_GETSESSION, dictionary);
        }
        /// <summary>
        /// 返回该App中的所有faceset
        /// </summary>
        /// <returns></returns>
        public FaceSetListInfo Info_GetFacesetList()
        {
            var dictionary = new Dictionary<object, object>();
            return HttpGet<FaceSetListInfo>(Constants.URL_INFO_GET_FACESETLIST, dictionary);
        }
        #endregion
        #region HTTP请求
        public T HttpGet<T>(string url, IDictionary<object, object> dictionary) where T : class
        {
            dictionary.Add("api_key", app_key);
            dictionary.Add("api_secret", app_secret);
            var query = dictionary.ToQueryString();
            logger.Error(url + "?" + query);
            var json = HttpMethod.HttpGet(url + "?" + query);
            return json.ToEntity<T>("json");
        }

        public string HttpGet(string url, IDictionary<object, object> dictionary)
        {
            dictionary.Add("api_key", app_key);
            dictionary.Add("api_secret", app_secret);
            var query = dictionary.ToQueryString();
            logger.Error(url + "?" + query);
            return HttpMethod.HttpGet(url + "?" + query);
        }
        public T HttpPost<T>(string url, IDictionary<object, object> dictionary) where T : class
        {
            return HttpPost<T>(url, dictionary, null);
        }

        private T HttpPost<T>(string url, IDictionary<object, object> dictionary, byte[] file) where T : class
        {
            dictionary.Add("api_key", app_key);
            dictionary.Add("api_secret", app_secret);
            var query = dictionary.ToQueryString();
            logger.Error(url);
            logger.Error(query);
            var json = string.Empty;
            if (file != null)
            {
                json = HttpMethod.HttpPost(url, dictionary, file);
            }
            else
            {
                json = HttpMethod.HttpPost(url, query);
            }

            return json.ToEntity<T>();
        }

        private string HttpPost(string url, IDictionary<object, object> dictionary)
        {
            dictionary.Add("api_key", app_key);
            dictionary.Add("api_secret", app_secret);
            var query = dictionary.ToQueryString();
            logger.Error(url);
            logger.Error(query);
            return HttpMethod.HttpPost(url, query);
        }
        #endregion
    }
}
