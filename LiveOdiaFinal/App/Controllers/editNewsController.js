LiveOdiaApp.controller('editNewsController', ['$scope', '$rootScope', '$location', '$anchorScroll', '$route', '$routeParams', '$window', '$uibModalInstance', 'homeServiceFactory', 'editNewsService', 'mobileCheck', 'impnews', function ($scope, $rootScope, $location, $anchorScroll, $route, $routeParams, $window, $uibModalInstance, homeServiceFactory, editNewsService, mobileCheck, impnews) {
    debugger;
    $scope.inews = impnews;
    $scope.editedHnews = [];
    //$scope.hotnewsDate = new Date();
    $scope.loader = {
        loading1: false,
        loading2: false,
        loading3: false,
        loading4: false
    };

    //$scope.minDate = new Date(
    //    $scope.myDate.getFullYear(),
    //    $scope.myDate.getMonth() - 2,
    //    $scope.myDate.getDate());

    //$scope.maxDate = new Date(
    //    $scope.myDate.getFullYear(),
    //    $scope.myDate.getMonth() + 2,
    //    $scope.myDate.getDate());

    //$scope.onlyWeekendsPredicate = function (date) {
    //    var day = date.getDay();
    //    return day === 0 || day === 6;
    //};


    $scope.ok = function () {
        debugger;
        $scope.loader.loading1 = true;
        //$scope.hotnews.hotnewsdt = $filter('date')(new Date($scope.hotnewsDate), 'dd-MM-yyyy');
        var file = {};
        if ($scope.myFile1 !== undefined) {
            file["file"] = $scope.myFile1;
        }
        file["inews"] = $scope.inews[0].impnews;
        file["isub"] = $scope.inews[0].isub;
        file["title"] = $scope.inews[0].ititle;
        file["ImpNews"] = "Inews";
        file["todaydate"] = $scope.inews[0].newsdate;
        file["myColor"] = $scope.inews[0].mycolor;
        file["priority"] = $scope.inews[0].priority;
        file["inid"] = $scope.inews[0].inid;

        editNewsService.uploadFileToUrl(file).then(function (data) {
            if (data === "") {
                $scope.loader.loading1 = false;
                $scope.mesgHnews = "Uploaded Successfully";
            }
            else {
                $scope.loader.loading1 = false;
                $scope.mesgHnews = "Not Successful";
            }
            $scope.myColor2 = "#000000";
            file = null;
            $scope.inews = [];
            $scope.resetForm("submitHotNews");
        });
        $uibModalInstance.close(file);

    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };

    $scope.resetForm = function (filereset) {
        if (filereset === "submitNewstory") {
            angular.element(document.querySelector('#file2')).val(null);
            $scope.myFile2 = undefined;
        }
        else if (filereset === "submitTopNews") {
            angular.element(document.querySelector('#file3')).val(null);
            $scope.myFile3 = undefined;
        }
        else if (filereset === "submitHotNews") {
            angular.element(document.querySelector('#file1')).val(null);
            $scope.myFile1 = undefined;
        }
        else if (filereset === "submitImpNews") {
            angular.element(document.querySelector('#file4')).val(null);
            $scope.myFile4 = undefined;
        }
    };

}]);