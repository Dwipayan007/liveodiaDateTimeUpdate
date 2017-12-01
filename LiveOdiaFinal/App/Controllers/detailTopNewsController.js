/// <reference path="../app.js" />
LiveOdiaApp.controller('detailTopNewsController', ['$scope', '$routeParams', 'detailTopnewsService', function ($scope, $routeParams, detailTopnewsService) {
    $scope.message = "Welcome to detail view page";
    $scope.detailNews = [];
    $scope.newsid = $routeParams.id;
    $scope.NextNews = function (newsid) {

    };

    $scope.GetTopDetailNews = function () {
        detailTopnewsService.getTopDetailNews($scope.newsid).then(function (dnewsdata) {
            if (dnewsdata) {
                $scope.detailNews = dnewsdata;
            }
        });
    };
    $scope.GetTopDetailNews();
}]);
