
LiveOdiaApp.factory('detailnewsServiceFactory', ['$http', '$q', 'baseService', function ($http, $q, baseService) {
   
    //var baseService = baseService;
    //var baseService = "http://www.liveodia.co/";
    var detailnewsServiceFactory = {};
    var _getDetailNews = function (id) {
       
        var deffer = $q.defer();
        $http.get(baseService + 'api/NewStory/' + id).success(function (data, status) {
           
            deffer.resolve(data);
        }).error(function (err, status) {
           
            deffer.reject(err);
        });
        return deffer.promise;
    };

    var _getFullNews = function (nid) {
       
        var deffer = $q.defer();
        $http.get(baseService + 'api/Fullnews/' + nid).success(function (data, status) {

            deffer.resolve(data);
        }).error(function (err, status) {

            deffer.reject(err);
        });

        return deffer.promise;
    };

    var _getHotFullNews = function (hnid) {
       
        var deffer = $q.defer();
        $http.get(baseService + 'api/Test' + hnid).success(function (data, status) {

            deffer.resolve(data);
        }).error(function (err, status) {

            deffer.reject(err);
        });

        return deffer.promise;
    };

    detailnewsServiceFactory.getDetailNews = _getDetailNews;
    detailnewsServiceFactory.getFullNews = _getFullNews;
    detailnewsServiceFactory.getHotFullNews = _getHotFullNews;

    return detailnewsServiceFactory;
}]);