/// <reference path="../app.js" />
LiveOdiaApp.factory('detailTopnewsService', ['$http', '$q', 'baseService', function ($http, $q, baseService) {
    //var baseService = "http://www.liveodia.co/";
    //var baseService = baseService;
    var detailTopnewsFactory = {};
    var _getTopDetailNews = function (id) {
       
        var deffer = $q.defer();
        $http.get(baseService + 'api/topDetailNews/' + id).success(function (data, status) {
           
            deffer.resolve(data);
        }).error(function (err, status) {
           
            deffer.reject(err);
        });
        return deffer.promise;
    };
    detailTopnewsFactory.getTopDetailNews = _getTopDetailNews;


    return detailTopnewsFactory;
}]);
