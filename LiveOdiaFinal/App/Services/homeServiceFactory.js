
LiveOdiaApp.factory('homeServiceFactory', ['$http', '$q', 'baseService', function ($http, $q, baseService) {
    //var baseService = "http://www.liveodia.co/";
    var homeServiceFactory = {};

 	var _getRelatedNews = function (rid) {
        var r = parseInt(rid);
        var deffer = $q.defer();
        $http.get(baseService + 'api/RelatedNews/' + r).success(function (data, status) {

            deffer.resolve(data);
        }).error(function (err, status) {

            deffer.reject(err);
        });
        return deffer.promise;
    };
    var _getAllNews = function () {
       
        var deffer = $q.defer();
        $http.get(baseService + 'api/home').success(function (data, status) {
           
            deffer.resolve(data);
        }).error(function (err, status) {
           
            deffer.reject(err);
        });
        return deffer.promise;
    };

    var _getImpNews = function () {
        var deffer = $q.defer();
        $http.get(baseService + 'api/ImpNews').success(function (data, status) {

            deffer.resolve(data);
        }).error(function (err, status) {

            deffer.reject(err);
        });
        return deffer.promise;
    };

    var _getImpNewsById = function (inid) {
        var deffer = $q.defer();
        $http.get(baseService + 'api/ImpNews/'+inid).success(function (data, status) {

            deffer.resolve(data);
        }).error(function (err, status) {

            deffer.reject(err);
        });
        return deffer.promise;
    };

    var _getAllNewsStory = function () {
       
        var deffer = $q.defer();
        $http.get(baseService + 'api/NewStory').success(function (data, status) {
           
            deffer.resolve(data);
        }).error(function (err, status) {
           
            deffer.reject(err);
        });
        return deffer.promise;
    };

  var _DeleteImpNews = function (id) {

        var deffer = $q.defer();
        $http.delete(baseService + 'api/ImpNews/' + id).success(function (data, status) {

            deffer.resolve(data);
        }).error(function (err, status) {

            deffer.reject(err);
        });
        return deffer.promise;
    };

    var _DeleteNewsStory = function (id) {
       
        var deffer = $q.defer();
        $http.delete(baseService + 'api/NewStory/' + id).success(function (data, status) {
           
            deffer.resolve(data);
        }).error(function (err, status) {
           
            deffer.reject(err);
        });
        return deffer.promise;
    };

    var _DeleteTopNews = function (id) {
       
        var deffer = $q.defer();
        $http.delete(baseService + 'api/home/' + id).success(function (data, status) {
           
            deffer.resolve(data);
        }).error(function (err, status) {
           
            deffer.reject(err);
        });
        return deffer.promise;
    };
    var _DeleteHotNews= function (id) {
       
        var deffer = $q.defer();
        $http.delete(baseService + 'api/HotNews/' + id).success(function (data, status) {
           
            deffer.resolve(data);
        }).error(function (err, status) {
           
            deffer.reject(err);
        });
        return deffer.promise;
    };

    var _getTopNewsByID = function (nid) {
       
        var deffer = $q.defer();
        $http.get(baseService + 'api/home/' + nid).success(function (data, status) {

            deffer.resolve(data);
        }).error(function (err, status) {

            deffer.reject(err);
        });

        return deffer.promise;
    };
 	var _getFullRnews = function (fnid) {
        var deffer = $q.defer();
        $http.get(baseService + 'api/FullNews/' + fnid).success(function (data, status) {

            deffer.resolve(data);
        }).error(function (err, status) {

            deffer.reject(err);
        });

        return deffer.promise;
    };
    var _getAllHotNews = function () {
       
        var deffer = $q.defer();
        $http.get(baseService + 'api/admin2/').success(function (data, status) {

            deffer.resolve(data);
        }).error(function (err, status) {

            deffer.reject(err);
        });

        return deffer.promise;
    };

    var _getHotNewsSummary = function (ndid) {
       
        var deffer = $q.defer();
        $http.get(baseService + 'api/HotNews/' + ndid).success(function (data, status) {

            deffer.resolve(data);
        }).error(function (err, status) {

            deffer.reject(err);
        });
        return deffer.promise;
    };

    homeServiceFactory.DeleteTopNews = _DeleteTopNews;
	homeServiceFactory.DeleteImpNews = _DeleteImpNews;
    homeServiceFactory.DeleteHotNews = _DeleteHotNews;
    homeServiceFactory.DeleteNewsStory = _DeleteNewsStory;
    homeServiceFactory.getImpNews = _getImpNews;
 	homeServiceFactory.getFullRnews = _getFullRnews;
    homeServiceFactory.getRelatedNews = _getRelatedNews;
    homeServiceFactory.GetImpNewsById = _getImpNewsById;
    homeServiceFactory.getAllHotNews = _getAllHotNews;
    homeServiceFactory.getAllNewsStory = _getAllNewsStory;
    homeServiceFactory.getAllNews = _getAllNews;
    homeServiceFactory.getTopNewsByID = _getTopNewsByID;
    homeServiceFactory.getHotNewsSummary = _getHotNewsSummary;

    return homeServiceFactory;
}]);