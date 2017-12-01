
LiveOdiaApp.controller('detailHotnewsController', ['$scope', '$rootScope', '$location', '$routeParams', '$anchorScroll', '$window','$uibModal', 'HotnewsServiceFactory', 'sharedService', 'homeServiceFactory', function ($scope, $rootScope, $location, $routeParams, $anchorScroll, $window,$uibModal, HotnewsServiceFactory, sharedService, homeServiceFactory) {
    $window.ga('send', 'detailhotnews', $location.path());
  	var matrixParams = getIds($routeParams.ids);
    $scope.newsid = matrixParams.ids;
    $scope.rid = matrixParams.rid;
    //$scope.newsid = $routeParams.id;
    $scope.viewActive = $rootScope.hideit;
    $scope.hnewsDetail = [];
    $scope.relatedNews = [];
    $scope.hnewsTitle = [];
    $scope.morenews = [];
    $scope.hotnews = [];
    $scope.topstories = [];
    $scope.hnews = false;
    $scope.rfnews = false;
    $scope.hnewssummary = false;
    $scope.tpnews = false;
    $scope.index = "";
    $scope.hnewsData = [];
    $scope.hnewsid = "";
    $scope.nHNews = "";
    $scope.pHNews = "";

	 $scope.editNewstory = function (hnid) {
        debugger;
        var modalInstance = $uibModal.open({
            templateUrl: 'editHotNews.html',
            controller: 'editHotNewsController',
            resolve: {
                hnewsDetail: function () {
                    return $scope.hnewsDetail;
                }
            }
        });
        modalInstance.result.then(function (selectedItem) {
            $scope.selected = selectedItem;
        }, function () {
            console.log('Modal dismissed at: ' + new Date() + " " + $scope.selected);
        });
    };

    $scope.getPreviousHotNews = function (newsid) {
        $window.ga('send', 'event', 'detailhotnews', 'PreviousHotNews');
        $scope.hnewsid = newsid;
		$scope.getHotNewsOnClick($scope.hnewsid, $scope.rid);
    };

    $scope.getNextHotNews = function (newsid) {
        $window.ga('send', 'event', 'detailhotnews', 'NextHotNews');
        $scope.hnewsid = newsid;
        $scope.getHotNewsOnClick($scope.hnewsid, $scope.rid);
    };


    $scope.getAllTopNews = function () {
        $window.ga('send', 'event', 'detailhotnews', 'All Top News Called');
        HotnewsServiceFactory.getAllTopNews().then(function (newsData) {
            if (newsData) {
                $scope.topstories = [];
                $scope.topstories = newsData;
            }
        });
    };

    $scope.getTopNewsByID = function (newsid, rid) {
		  $scope.rid = rid;
 		
            $('html, body').animate({ scrollTop: $("#middle_content").offset().top-100 }, 2000);
			$('body,html').bind('scroll mousedown wheel DOMMouseScroll mousewheel keyup touchmove', function(e){
				if ( e.which > 0 || e.type == "mousedown" || e.type == "mousewheel" || e.type == "touchmove"){
					$("html,body").stop();
				}
        	});
  		if (rid != 0 && rid != null) {
            homeServiceFactory.getRelatedNews(rid).then(function (rnews) {
                $scope.relatedNews = rnews;
            });
        }
        $window.ga('send', 'event', 'detailhotnews', 'Top News By Id');
        homeServiceFactory.getTopNewsByID(newsid).then(function (result) {
            if (result) {
                $scope.tpnews = true;
                $scope.rfnews = false;
                $scope.hnews = false;
                $scope.hnewssummary = false;
                $scope.ipnews = false;
                $scope.hnewssummary = [];
               
                $scope.tnews = result;
				if ($scope.tnews[0].mycolor) {
                    $scope.CustomStyle = {
                        'color': $scope.tnews[0].mycolor
                    };
                }
            }
        });
    };

    $scope.GetImpNewsById = function (inid, rid) {
        $scope.rid = rid;
      
            $('html, body').animate({ scrollTop: $("#middle_content").offset().top - 100 }, 2000);
            $('body,html').bind('scroll mousedown wheel DOMMouseScroll mousewheel keyup touchmove', function (e) {
                if (e.which > 0 || e.type == "mousedown" || e.type == "mousewheel" || e.type == "touchmove") {
                    $("html,body").stop();
                }
            });
        if (rid != 0 || rid != null) {
            homeServiceFactory.getRelatedNews(rid).then(function (rnews) {
                debugger;
                $scope.relatedNews = rnews;
            });
        }
        $window.ga('send', 'event', 'home', 'Get Imp News by Id');
        homeServiceFactory.GetImpNewsById(inid).then(function (newsData) {
            if (newsData) {
                $scope.imnews = newsData;
                if ($scope.imnews[0].mycolor) {
                    $scope.CustomStyle = {
                        'color': $scope.imnews[0].mycolor
                    };
                }
                $scope.ipnews = true;
                $scope.tpnews = false;
                $scope.rfnews = false;
                $scope.hnews = false;
                $scope.hnewssummary = false;
            }
        });
    };

    $scope.getImpNews = function () {
        homeServiceFactory.getImpNews().then(function (newsData) {
            if (newsData) {
                $scope.inews = _.find(newsData, { priority: "1" });
                $scope.impnews = newsData;
                if ($scope.impnews[0].mycolor) {
                    $scope.CustomStyle = {
                        'color': $scope.impnews[0].mycolor
                    };
                }
                $scope.vinews = _.reject($scope.impnews, { priority: null });
                $scope.vinews = _.reject($scope.vinews, { priority: "1" });
                $scope.vvnews = _.reject($scope.impnews, { priority: null });
                $scope.morenews = $scope.impnews.splice(0, $scope.impnews.length - 5);
            }
        });
    };

    $scope.getHotNewsSummaryData = function (ndid) {
        homeServiceFactory.getHotNewsSummary(ndid).then(function (hnewsdata) {
            if (hnewsdata) {
                if (ndid === 1)
                    $scope.hnewsData1 = hnewsdata;
                else if (ndid === 2)
                    $scope.hnewsData2 = hnewsdata;
                else if (ndid === 3)
                    $scope.hnewsData3 = hnewsdata;
                else if (ndid === 4)
                    $scope.hnewsData4 = hnewsdata;
                else if (ndid === 5)
                    $scope.hnewsData5 = hnewsdata;
                else if (ndid === 6)
                    $scope.hnewsData6 = hnewsdata;
                else if (ndid === 7)
                    $scope.hnewsData7 = hnewsdata;
            }
        });
    };
    $scope.getHotNewsTitle = function () {
        $window.ga('send', 'event', 'detailhotnews', 'All Hot News Title');
        HotnewsServiceFactory.getHotFullNewsTitle().then(function (hnewsdata) {
            if (hnewsdata) {

                $scope.hnewsTitle = [];
                $scope.hnewsTitle = hnewsdata;
 				for (var i = 0; i < $scope.hnewsTitle.length; i++)
                    $scope.getHotNewsSummaryData($scope.hnewsTitle[i].ndid);
            }
        });
    };
    $scope.getFullRnews = function (fnid) {
		$('html, body').animate({ scrollTop: $("#middle_content").offset().top-100 }, 2000);
			$('body,html').bind('scroll mousedown wheel DOMMouseScroll mousewheel keyup touchmove', function(e){
				if ( e.which > 0 || e.type == "mousedown" || e.type == "mousewheel" || e.type == "touchmove"){
					$("html,body").stop();
				}
        	});
        homeServiceFactory.getFullRnews(fnid).then(function (newsData) {
            if (newsData) {
                $scope.rnewsdata = newsData;
                if ($scope.rnewsdata[0].mycolor) {
                    $scope.CustomStyle = {
                        'color': $scope.rnewsdata[0].mycolor
                    };
                }
                $scope.ipnews = false;
                $scope.tpnews = false;
                $scope.hnews = false;
                $scope.nstory = false;
                $scope.rfnews = true;
                $scope.hnewssummary = false;

            }
        });
    };
															 
    $scope.getHotNewsByID = function () {
            $('html, body').animate({ scrollTop: $("#middle_content").offset().top-100 }, 2000);
			$('body,html').bind('scroll mousedown wheel DOMMouseScroll mousewheel keyup touchmove', function(e){
				if ( e.which > 0 || e.type == "mousedown" || e.type == "mousewheel" || e.type == "touchmove"){
					$("html,body").stop();
				}
        	});
        $window.ga('send', 'event', 'detailhotnews', 'Hot News By Id');
        $scope.hnewsid = $scope.newsid;
		if ($scope.rid != 0 && $scope.rid != null) {
            homeServiceFactory.getRelatedNews($scope.rid).then(function (rnews) {
                $scope.relatedNews = rnews;
            });
        }

        HotnewsServiceFactory.getHotNewsSummary($scope.newsid).then(function (hnewsdata) {
            if (hnewsdata) {

                $scope.hnewsDetail = [];
                $scope.hnewssummary = true;
                $scope.ipnews = false;
                $scope.hnews = false;
                $scope.tpnews = false;
                $scope.rfnews = false;
                $scope.hnewsDetail = hnewsdata[0];
                $scope.getHotNewsTitle();
                $scope.getAllTopNews();
                $scope.getHotNewsData();
            }
        });
    };

    $scope.getHotNewsOnClick = function (hnid, rid) {
            $('html, body').animate({ scrollTop: $("#middle_content").offset().top-100 }, 2000);
			$('body,html').bind('scroll mousedown wheel DOMMouseScroll mousewheel keyup touchmove', function(e){
				if ( e.which > 0 || e.type == "mousedown" || e.type == "mousewheel" || e.type == "touchmove"){
					$("html,body").stop();
				}
        	});
        $scope.rid = rid;
        if ($scope.rid != 0 && $scope.rid != null) {
            homeServiceFactory.getRelatedNews($scope.rid).then(function (rnews) {
                $scope.relatedNews = rnews;
            });
        }
        $window.ga('send', 'event', 'detailhotnews', 'Hot News Clicked');
        $scope.hnewsid = hnid;
        HotnewsServiceFactory.getHotNewsByID(hnid).then(function (result) {
            if (result) {
                $scope.hnews = false;
                $scope.ipnews = false;
                $scope.tpnews = false;
                $scope.hnewssummary = [];
                $scope.hnewssummary = true;
                $scope.rfnews = false;
                $scope.hnewsDetail = result[0];
                $scope.getHotNewsData();
            }
        });
    };

    $scope.getHotNewsData = function () {
        homeServiceFactory.getAllHotNews().then(function (hnewsData) {
            if (hnewsData) {
                $scope.hnewsData = hnewsData;
                if ($scope.hnewsid !== "") {
                    $scope.index = _.findIndex($scope.hnewsData, { "hnid": parseInt($scope.hnewsid) });
                    $scope.pHNews = $scope.hnewsData[$scope.index - 1];
                    $scope.nHNews = $scope.hnewsData[$scope.index + 1];
                }
            }
        });
    };
        var trigger = $('.hamburger'),
            overlay = $('.overlay'),
           isClosed = false;

        trigger.click(function () {
            hamburger_cross();
        });

        function hamburger_cross() {
            $window.ga('send', 'event', 'home', 'Mobile Menu Closed');
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

    $scope.getHotNewsSummary = function (ndid) {
            $('html, body').animate({ scrollTop: $("#middle_content").offset().top-100 }, 2000);
			$('body,html').bind('scroll mousedown wheel DOMMouseScroll mousewheel keyup touchmove', function(e){
				if ( e.which > 0 || e.type == "mousedown" || e.type == "mousewheel" || e.type == "touchmove"){
					$("html,body").stop();
				}
        	});
		$(".hamburger").trigger("click");
        $window.ga('send', 'event', 'detailhotnews', 'hot news Summary');
        homeServiceFactory.getHotNewsSummary(ndid).then(function (hnewsdata) {
            if (hnewsdata) {
                $scope.hnewssummary = false;
                $scope.ipnews = false;
                $scope.hnews = true;
                $scope.tpnews = false;
                $scope.rfnews = false;
                $scope.hotnews = hnewsdata;
            }
        });
    };
 	$scope.getHotNewsTitle();
    $scope.getHotNewsByID();
    $scope.getImpNews();
}]);