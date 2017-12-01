LiveOdiaApp.controller('homeController', ['$scope', '$rootScope', '$location', '$anchorScroll', '$route', '$routeParams', '$window', '$uibModal', '$timeout', 'homeServiceFactory', 'HotnewsServiceFactory', 'mobileCheck', function ($scope, $rootScope, $location, $anchorScroll, $route, $routeParams, $window, $uibModal, $timeout,homeServiceFactory, HotnewsServiceFactory, mobileCheck) {
    $window.ga('send', 'HomePage', $location.path());
    $scope.mobile = mobileCheck;
    $scope.hotNews = [];
	$scope.vvnews=[];
    $scope.topstories = [];
    $scope.newsories = [];
    $scope.filteredNews = [];
    $scope.currentPage = 1;
    $scope.numPerPage = 10;
    $scope.maxSize = 5;
    $scope.fullnews = [];
	$scope.vinews=[];
    $scope.cleanData = [];
    $scope.topnews = [];
    $scope.hotnews = [];
    $scope.newstory = [];
	$scope.hnewsData=[];
	$scope.morenews=[];
	$scope.relatedNews = [];
    $scope.letterLimit = 120;
    $scope.impnewsLimit = 10;
    $scope.tpnews = false;
 	$scope.rid = null;
  	$scope.rfnews = false;
    $scope.hnews = false;
    $scope.nstory = false;
    $scope.ipnews = false;
    $scope.isClosed = false;
	$scope.hnewsdetail=false;
    $scope.impnews = [];
    $scope.imnews = [];
	 $scope.open = function (hnid) {
        debugger;
        var modalInstance = $uibModal.open({
            templateUrl: 'editnews.html',
            controller: 'editNewsController',
            resolve: {
                impnews: function () {
                    return $scope.imnews;
                }
            }
        });
        modalInstance.result.then(function (selectedItem) {
            $scope.selected = selectedItem;
        }, function () {
            console.log('Modal dismissed at: ' + new Date() + " " + $scope.selected);
        });
    };

    //Trigger function to Open the modal 
    $scope.editTopNews = function (hnid) {
        var modalInstance = $uibModal.open({
            templateUrl: 'editTopNews.html',
            controller: 'editTopNewsController',
            resolve: {
                tnews: function () {
                    return $scope.tnews;
                }
            }
        });
        modalInstance.result.then(function (selectedItem) {
            $scope.selected = selectedItem;
        }, function () {
            console.log('Modal dismissed at: ' + new Date() + " " + $scope.selected);
        });
    };

    //End Edit News

    $scope.CollapseNews = function ($event) {
        var a = jQuery($event.target);
        var p = a.next(".tab-content")[0];
        p.stop().slideToggle(300);
    }

    //Fb Share 
    $timeout(function () {
        $scope.url = 'http://google.com';
        $scope.text = 'testing share';
        $scope.title = 'title1'
    }, 1000)
    $timeout(function () {
        $scope.url = 'https://www.youtube.com/watch?v=wxkdilIURrU';
        $scope.text = 'testing second share';
        $scope.title = 'title2';
    }, 1000)

    $scope.callback = function (response) {
        console.log(response);
        alert('share callback');
    }
    //FB Share ends



    $scope.clicked = function ($event) {
        var a = jQuery($event.target);
        var p = a.next("#panel");
        p.stop().slideToggle(300);
    };

    
    /// <summary>
    /// Get Important News by id and make the ipnews true for middle view Both in mobile and Desktop With related news
    /// </summary>
    
    $scope.GetImpNewsById = function (inid, rid) {
$scope.hnewsdetail = false;
        $scope.ipnews = true;
        $scope.tpnews = false;
        $scope.hnews = false;
        $scope.nstory = false;
        $scope.rfnews = false;
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
                $scope.hnews = false;
                $scope.nstory = false;
                $scope.rfnews = false;
            }
        });
    };

    /// <summary>
    /// Get Full News by id and make the ipnews true for middle view Both in mobile and Desktop With related news
    /// </summary>
    $scope.getFullRnews = function (fnid) {
        $scope.ipnews = false;
        $scope.tpnews = false;
        $scope.hnews = false;
        $scope.nstory = false;
        $scope.rfnews = true;
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
				$('html, body').animate({ scrollTop: $("#middle_content").offset().top - 100 }, 2000);
				$('body,html').bind('scroll mousedown wheel DOMMouseScroll mousewheel keyup touchmove', function (e) {
					if (e.which > 0 || e.type == "mousedown" || e.type == "mousewheel" || e.type == "touchmove") {
						$("html,body").stop();
					}
				});
            }
        });
    };
   
    /// <summary>
    /// Get All Important News
    /// </summary>
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

    /// <summary>
    /// Delete Important News By important news ID ie INID
    /// </summary>
    $scope.DeleteImpNews = function (inid) {
        $scope.ipnews = false;
        $scope.tpnews = false;
        $scope.hnews = false;
        $scope.nstory = true;
        homeServiceFactory.DeleteImpNews(inid).then(function (res) {
            if (res) {
                $scope.getAllNews();
                $scope.ipnews = false;
                $scope.tpnews = false;
                $scope.hnews = false;
                $scope.nstory = true;
            }
        });
    }

    /// <summary>
    /// Delete New Story By New Story news ID ie NSID
    /// </summary>
    $scope.DeleteNewsStory = function (nsid) {
        $scope.ipnews = false;
        $scope.tpnews = false;
        $scope.hnews = false;
        $scope.nstory = true;
        homeServiceFactory.DeleteNewsStory(nsid).then(function (res) {
            if (res) {
                $scope.getAllNews();
                $scope.ipnews = false;
                $scope.tpnews = false;
                $scope.hnews = false;
                $scope.nstory = true;
            }
        });
    };

    /// <summary>
    /// Delete New Story By New Story news ID ie NSID
    /// </summary>
    $scope.DeleteTopNews = function (tnid) {
        $scope.tpnews = true;
        $scope.ipnews = false;
        $scope.hnews = false;
        $scope.nstory = false;
        homeServiceFactory.DeleteTopNews(tnid).then(function (res) {
            if (res) {
                $scope.getAllNews();
                $scope.tpnews = true;
                $scope.ipnews = false;
                $scope.hnews = false;
                $scope.nstory = false;
            }
        });
    };

    /// <summary>
    /// Delete Hot News By Hot News news ID ie HNID
    /// </summary>
    $scope.DeleteHotNews = function (hnid) {
        $scope.tpnews = false;
        $scope.ipnews = false;
        $scope.hnews = false;
        $scope.nstory = true;
        homeServiceFactory.DeleteHotNews(hnid).then(function (res) {
            if (res) {
                $scope.getAllNews();
                $scope.tpnews = false;
                $scope.ipnews = false;
                $scope.hnews = false;
                $scope.nstory = true;
            }
        });
    };

    /// <summary>
    /// Get All News By NEW Story 
    /// </summary>
    $scope.getAllNews = function () {
        $scope.nstory = true;
        $scope.ipnews = false;
        homeServiceFactory.getAllNews().then(function (newsData) {
            if (newsData) {
                $scope.topstories = newsData;
                $scope.getHotNewsTitle();
            }
        });
        homeServiceFactory.getAllNewsStory().then(function (newsData) {
            if (newsData) {
                $scope.nstory = true;
                $scope.ipnews = false;
                $scope.newstory = newsData;
            }
        });
    };

    //Get top news by tnid
    $scope.getTopNewsByID = function (newsid, rid) {
        $scope.tpnews = true;
        $scope.ipnews = false;
        $scope.hnews = false;
        $scope.nstory = false;
        $scope.rfnews = false;
        $scope.hnewsdetail = false;
        $scope.rid = rid;
            $('html, body').animate({ scrollTop: $("#middle_content").offset().top - 100 }, 2000);
            $('body,html').bind('scroll mousedown wheel DOMMouseScroll mousewheel keyup touchmove', function (e) {
                if (e.which > 0 || e.type == "mousedown" || e.type == "mousewheel" || e.type == "touchmove") {
                    $("html,body").stop();
                }
            });
        if (rid != 0 && rid != null) {
            homeServiceFactory.getRelatedNews(rid).then(function (rnews) {
                $scope.relatedNews = rnews;
            });
        }
        $window.ga('send', 'event', 'home', 'Get Top News by Id');
        homeServiceFactory.getTopNewsByID(newsid).then(function (result) {
            if (result) {
                $scope.tpnews = true;
                $scope.ipnews = false;
                $scope.hnews = false;
                $scope.nstory = false;
                $scope.rfnews = false;
                $scope.hnewsdetail = false;
                $scope.tnews = result;
                if ($scope.tnews[0].mycolor) {
                    $scope.CustomStyle = {
                        'color': $scope.tnews[0].mycolor
                    };
                }
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

    //Get Title for hot news
    $scope.getHotNewsTitle = function () {
        HotnewsServiceFactory.getHotFullNewsTitle().then(function (hnewsdata) {
            if (hnewsdata) {
                $scope.hotNews = hnewsdata;
                for (var i = 0; i < $scope.hotNews.length; i++)
                    $scope.getHotNewsSummaryData($scope.hotNews[i].ndid);
            }
        });
    };

    var trigger = $('.hamburger'),
        overlay = $('.overlay');

    trigger.click(function () {
        hamburger_cross();
    });

    function hamburger_cross() {
        $window.ga('send', 'event', 'home', 'Mobile Menu Clicked');
        if ($scope.isClosed === true) {
            overlay.hide();
            trigger.removeClass('is-open');
            trigger.addClass('is-closed');
            $scope.isClosed = false;
        } else {
            overlay.show();
            trigger.removeClass('is-closed');
            trigger.addClass('is-open');
            $scope.isClosed = true;
        }
    }

    $('[data-toggle="offcanvas"]').click(function () {
        $('#wrapper').toggleClass('toggled');
    });

    $scope.getHotNewsSummaryLast = function (ndid) {
        $scope.hnewsdetail = false;
        $scope.tpnews = false;
        $scope.ipnews = false;
        $scope.nstory = false;
        $scope.hnews = true;
        $window.ga('send', 'event', 'home', 'Get Hot News by Id');
        if ($scope.mobile) {
            $('html, body').animate({ scrollTop: $("#middle_content").offset().top - 100 }, 2000);
        }
        homeServiceFactory.getHotNewsSummary(ndid).then(function (hnewsdata) {
            if (hnewsdata) {

                $scope.hnewsdetail = false;
                $scope.tpnews = false;
                $scope.ipnews = false;
                $scope.nstory = false;
                $scope.hnews = true;
                $scope.hnewsDetail = hnewsdata;
            }
        });
    };

    $scope.getHotNewsByID = function (hnid) {
        $scope.hnewsdetail = true;
        $scope.tpnews = false;
        $scope.ipnews = false;
        $scope.nstory = false;
        $scope.hnews = false;
        $scope.rfnews = false;
            $('html, body').animate({ scrollTop: $("#middle_content").offset().top - 100 }, 2000);
            $('body,html').bind('scroll mousedown wheel DOMMouseScroll mousewheel keyup touchmove', function (e) {
                if (e.which > 0 || e.type == "mousedown" || e.type == "mousewheel" || e.type == "touchmove") {
                    $("html,body").stop();
                }
            });
        $window.ga('send', 'event', 'detailhotnews', 'Hot News By Id');
        HotnewsServiceFactory.getHotNewsSummary(hnid).then(function (hnewsdata) {
            if (hnewsdata) {

                $scope.hnewsDetail = [];
                $scope.hnewsdetail = true;
                $scope.tpnews = false;
                $scope.ipnews = false;
                $scope.nstory = false;
                $scope.hnews = false;
                $scope.rfnews = false;
                $scope.hnewsDetail = hnewsdata;
            }
        });
    };

    //get All hot summary news by ndid
    $scope.getHotNewsSummary = function (ndid) {
        $scope.tpnews = false;
        $scope.ipnews = false;
        $scope.nstory = false;
        $scope.hnews = true;
        $scope.rfnews = false;
            $('html, body').animate({ scrollTop: $("#middle_content").offset().top - 100 }, 2000);
            $('body,html').bind('scroll mousedown wheel DOMMouseScroll mousewheel keyup touchmove', function (e) {
                if (e.which > 0 || e.type == "mousedown" || e.type == "mousewheel" || e.type == "touchmove") {
                    $("html,body").stop();
                }
            });
        $(".hamburger").trigger("click");
        $window.ga('send', 'event', 'home', 'Get Hot News by Id');
        homeServiceFactory.getHotNewsSummary(ndid).then(function (hnewsdata) {
            if (hnewsdata) {
                $scope.tpnews = false;
                $scope.ipnews = false;
                $scope.nstory = false;
                $scope.hnews = true;
                $scope.rfnews = false;
                $scope.hnewsDetail = hnewsdata;
            }
        });
    };

    $scope.getImpNews();

    $scope.getAllNews();

    $(document).ready(function () {
        var clickEvent = false;
        $('#myCarousel').carousel({
            interval: 4000
        }).on('click', '.list-group li', function () {
            clickEvent = true;
            $('.list-group li').removeClass('active');
            $(this).addClass('active');
        }).on('slid.bs.carousel', function (e) {
            if (!clickEvent) {
                var count = $('.list-group').children().length - 1;
                var current = $('.list-group li.active');
                current.removeClass('active').next().addClass('active');
                var id = parseInt(current.data('slide-to'));
                if (count == id) {
                    $('.list-group li').first().addClass('active');
                }
            }
            clickEvent = false;
        });
    })

    $(window).load(function () {
        var boxheight = $('#myCarousel .carousel-inner').innerHeight();
        var itemlength = $('#myCarousel .item').length;
        var triggerheight = Math.round(boxheight / itemlength + 1);
        $('#myCarousel .list-group-item').outerHeight(triggerheight);
    });

}]);
LiveOdiaApp.directive('slideToggle', function () {
    return {
        restrict: 'A',
        scope: {},
        controller: function ($scope) {
            $scope.togle = true;
        },
        link: function (scope, element, attr) {
            element.bind('click', function () {
                var $slideBox = jQuery(attr.slideToggle);
                var slideDuration = parseInt(attr.slideToggleDuration, 10) || 200;
                $slideBox.stop().slideToggle(slideDuration);
            });
        }
    };
});