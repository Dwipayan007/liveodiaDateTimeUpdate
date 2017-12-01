
LiveOdiaApp.controller('indexController', ['$scope', '$rootScope', '$location', '$anchorScroll', 'homeServiceFactory', 'HotnewsServiceFactory', 'loginServiceFactory', 'mobileCheck', function ($scope, $rootScope, $location, $anchorScroll, homeServiceFactory,HotnewsServiceFactory, loginServiceFactory, mobileCheck) {
    
    $scope.mobile = mobileCheck;
    //if ($scope.mobile) {
    //    
    //    $location.hash('middle');
    //    $anchorScroll.yOffset = 20;
    //    $anchorScroll();
    //}
    $scope.viewActive = $rootScope.hideit;
    //logout 
    $scope.logOut = function () {
        loginServiceFactory.logOut();
        $location.path('/home');
    };
    $scope.onHomeClick = function () {
  		if ($scope.mobile) {
            $('html, body').animate({ scrollTop: $("#middle_content").offset().top - 100 }, 2000);
            $('body,html').bind('scroll mousedown wheel DOMMouseScroll mousewheel keyup touchmove', function (e) {
                if (e.which > 0 || e.type == "mousedown" || e.type == "mousewheel" || e.type == "touchmove") {
                    $("html,body").stop();
                }
            });
        }
        $location.path('/about');
    };
    
   

    $scope.authentication = loginServiceFactory.authentication;

}]);