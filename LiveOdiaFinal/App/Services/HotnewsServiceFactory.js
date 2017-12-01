
LiveOdiaApp.factory('HotnewsServiceFactory', ['$http', '$q', 'baseService', function ($http, $q, baseService) {
   
    //var baseService = baseService;
    //var baseService = baseService;

    var HotnewsServiceFactory = {};

    var _getHotFullNews = function (hnid) {
       
        var deffer = $q.defer();
        $http.get(baseService + 'api/Home/' + hnid).success(function (data, status) {
            deffer.resolve(data);
        }).error(function (err, status) {
            deffer.reject(err);
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

    var _getHotNewsSummary = function (hnid) {
       
        var deffer = $q.defer();
        $http.get(baseService + 'api/HnewsSummary/' + hnid).success(function (data, status) {
            deffer.resolve(data);
        }).error(function (err, status) {
            deffer.reject(err);
        });
        return deffer.promise;
    };

    var _getHotNewsByID = function (hnid) {
       
        var deffer = $q.defer();
        $http.get(baseService + 'api/HnewsSummary/' + hnid).success(function (data, status) {
            deffer.resolve(data);
        }).error(function (err, status) {
            deffer.reject(err);
        });
        return deffer.promise;
    };

    var _getHotNews = function (hnid) {
       
        var deffer = $q.defer();
        $http.get(baseService + 'api/home/' + hnid).success(function (data, status) {
            deffer.resolve(data);
        }).error(function (err, status) {
            deffer.reject(err);
        });
        return deffer.promise;
    };

    var _getAllTopNews = function () {
       
        var deffer = $q.defer();
        $http.get(baseService + 'api/HnewsSummary/').success(function (data, status) {
            deffer.resolve(data);
        }).error(function (err, status) {
            deffer.reject(err);
        });
        return deffer.promise;
    };

    HotnewsServiceFactory.getHotNewsByID = _getHotNewsByID;
    HotnewsServiceFactory.getAllTopNews = _getAllTopNews;
    HotnewsServiceFactory.getHotFullNews = _getHotFullNews;
    HotnewsServiceFactory.getHotFullNewsTitle = _getHotFullNewsTitle;
    HotnewsServiceFactory.getHotNewsSummary = _getHotNewsSummary;
    HotnewsServiceFactory.getHotNews = _getHotNews;
    return HotnewsServiceFactory;
}]);