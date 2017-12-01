LiveOdiaApp.controller('editTopNewsController', ['$scope', '$uibModalInstance', 'editNewsService',  'tnews', function ($scope,  $uibModalInstance,editNewsService,tnews) {
    debugger;
    $scope.tnews = tnews;
    $scope.editedHnews = [];
    //$scope.hotnewsDate = new Date();
    $scope.loader = {
        loading1: false,
        loading2: false,
        loading3: false,
        loading4: false
    };
    $scope.myColor = $scope.tnews[0].mycolor;
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
        if ($scope.myFile3 !== undefined) {
            file["file"] = $scope.myFile3;
        }
        file["topnews"] = $scope.tnews[0].topnews;
        file["tsub"] = $scope.tnews[0].tsub;
        file["ttitle"] = $scope.tnews[0].ttitle;
        file["TopNews"] = "Topnews";
        file["todaydate"] = $scope.tnews[0].newsdate;
        file["myColor"] = $scope.myColor;
        file["priority"] = $scope.tnews[0].priority;
        file["tnid"] = $scope.tnews[0].tnid;

        editNewsService.uploadFileToUrl(file).then(function (data) {
            if (data === "") {
                $scope.loader.loading2 = false;
                $scope.mesgHnews = "Uploaded Successfully";
            }
            else {
                $scope.loader.loading2 = false;
                $scope.mesgHnews = "Not Successful";
            }
            $scope.myColor = "#000000";
            file = null;
            $scope.tnews = [];
            $scope.resetForm("submitTopNews");
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