
LiveOdiaApp.factory('editNewsService', ['$http', '$q', 'baseService', function ($http, $q, baseService) {
    //var baseService = baseService;
    //var baseService = "http://www.liveodia.co/";
    var adminServiceFactory = {};
    //File Upload
    var getModelAsFormData = function (data) {

        var dataAsFormData = new FormData();
        if (data.hotNews) {

            //file["hnews"] = $scope.hnewsDetail.hotnews;
            //file["hsub"] = $scope.hnewsDetail.hsub;
            //file["title"] = $scope.hnewsDetail.htitle;
            //file["hotNews"] = "HotNews";
            //file["hotnewsdt"] = $scope.hnewsDetail.newsdate;
            //file["myColor"] = $scope.myColor;
            //file["priority"] = $scope.hnewsDetail.priority;
            //file["hnid"] = $scope.hnewsDetail.hnid;


           // dataAsFormData.append('updatedon', data.hotnewsdt);
            dataAsFormData.append('file', data.file);
            dataAsFormData.append("hotNews", data.hotNews);
            dataAsFormData.append("title", data.title);
            dataAsFormData.append('myColor', data.myColor);
            if (data.hsub)
                dataAsFormData.append("hsub", data.hsub);
            //dataAsFormData.append("selOption", data.selOption);
            dataAsFormData.append("priority", data.priority);
            dataAsFormData.append("hnews", data.hnews);
            dataAsFormData.append("hnid", data.hnid);
        }
        if (data.Newstory) {
           // dataAsFormData.append('updatedon', data.todaydate);
            dataAsFormData.append('file', data.file);
            dataAsFormData.append("Newstory", data.Newstory);
            dataAsFormData.append("title", data.title);
            dataAsFormData.append('myColor', data.myColor);
            if (data.nsub)
                dataAsFormData.append("nsub", data.nsub);
            dataAsFormData.append("nstory", data.nstory);
            dataAsFormData.append("priority", data.priority);
            dataAsFormData.append("nsid", data.nsid);
        }
        if (data.TopNews) {
            if (data.relatedto)
                dataAsFormData.append("relatedTo", data.relatedto);
            //dataAsFormData.append('updatedon', data.todaydate);
            dataAsFormData.append('file', data.file);
            dataAsFormData.append("Topnews", data.TopNews);
            dataAsFormData.append("title", data.ttitle);
            dataAsFormData.append('myColor', data.myColor);
            if (data.tsub)
                dataAsFormData.append("tsub", data.tsub);
            dataAsFormData.append("tnews", data.topnews);
            dataAsFormData.append("priority", data.priority);
            dataAsFormData.append("tnid", data.tnid);
        }
        if (data.ImpNews) {
            if (data.relatedto)
                dataAsFormData.append("relatedTo", data.relatedto);
            //dataAsFormData.append('updatedon', data.impdate);
            dataAsFormData.append('file', data.file);
            dataAsFormData.append("inews", data.inews);
            dataAsFormData.append("title", data.title);
            dataAsFormData.append("ImpNews", data.ImpNews);
            if (data.isub)
                dataAsFormData.append("isub", data.isub);
            dataAsFormData.append('myColor', data.myColor);
            dataAsFormData.append("priority", data.priority);
            dataAsFormData.append("inid", data.inid);
        }
       
        return dataAsFormData;
    };
    //File Upload Service
    var _uploadFileToUrl = function (file) {

        var deffer = $q.defer();
        $http({
            url: baseService + 'api/EditNews/',
            method: "POST",
            data: getModelAsFormData(file),
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        }).success(function (result, status) {

            deffer.resolve(result, status);
        }).error(function (result, status) {
            deffer.reject(result);
        });
        return deffer.promise;
    };


  

    var _getHotFullNewsTitle = function () {

        var deffer = $q.defer();
        $http.get(baseService + 'api/HotNews').success(function (data, status) {
            deffer.resolve(data);
        }).error(function (err, status) {
            deffer.reject(err);
        });
        return deffer.promise;
    };

  
  

   
    adminServiceFactory.getHotFullNewsTitle = _getHotFullNewsTitle;
    adminServiceFactory.uploadFileToUrl = _uploadFileToUrl;

    return adminServiceFactory;
}]);