
LiveOdiaApp.controller('hotnewsController', ['$scope', '$rootScope', '$location', '$routeParams', '$anchorScroll', 'HotnewsServiceFactory', 'sharedService', 'mobileCheck', function ($scope, $rootScope, $location, $routeParams,$anchorScroll, HotnewsServiceFactory, sharedService, mobileCheck) {
    
    $scope.mobile = mobileCheck;
    //$scope.newsid = $routeParams.id;
    //$scope.viewActive = $rootScope.hideit;
    //$scope.hfnewsDetail = [];
    //$scope.hnewsDetail = [];

    //$scope.getHotFullNewsTitle = function () {
    //    
    //    HotnewsServiceFactory.getHotFullNewsTitle().then(function (hnewsdata) {
    //        if (hnewsdata) {
    //            
    //            $scope.hnewsDetail = hnewsdata;
    //            $scope.getHDetailNews();
    //        };
    //    });
    //};

    //$scope.getHotNewsDetail= function (hid) {
    //    
    //    sharedService.getHotDetailNews(hid);
    //    $scope.getHDetailNews();
    //};

    //$scope.getHDetailNews = function () {
    //    
    //    //var hnid = sharedService.broadcastHdetailNews();
    //    HotnewsServiceFactory.getHotNews($scope.newsid).then(function (hfullnews) {
    //        if (hfullnews) {
    //            $scope.hfnewsDetail = hfullnews;
    //        }
    //    });
    //};
    //$scope.getHotFullNewsTitle();
    

    if ($scope.mobile) {
        
        $location.hash('page-content-wrapper');
        $anchorScroll.yOffset = 20;
        $anchorScroll();
    }

    $(document).ready(function () {
        
        var trigger = $('.hamburger'),
            overlay = $('.overlay'),
           isClosed = false;

        trigger.click(function () {
            hamburger_cross();
        });

        function hamburger_cross() {

            if (isClosed === true) {
                overlay.hide();
                trigger.removeClass('is-open');
                trigger.addClass('is-closed');
                isClosed = false;
            } else {
                overlay.show();
                trigger.removeClass('is-closed');
                trigger.addClass('is-open');
                isClosed = true;
            }
        }

        $('[data-toggle="offcanvas"]').click(function () {
            $('#wrapper').toggleClass('toggled');
        });
    });


}]);