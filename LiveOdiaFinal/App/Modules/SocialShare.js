﻿angular.module('socialshare', [])
	.factory('$FB', ['$window', function ($window) {
	    return {
	        init: function (fbId) {
	            if (fbId) {
	                this.fbId = fbId;
	                $window.fbAsyncInit = function () {
	                    FB.init({
	                        appId: fbId,
	                        channelUrl: 'app/channel.html',
	                        status: true,
	                        xfbml: true
	                    });
	                };
	                (function (d) {
	                    var js,
							id = 'facebook-jssdk',
							ref = d.getElementsByTagName('script')[0];
	                    if (d.getElementById(id)) {
	                        return;
	                    }

	                    js = d.createElement('script');
	                    js.id = id;
	                    js.async = true;
	                    js.src = "//connect.facebook.net/en_US/all.js";

	                    ref.parentNode.insertBefore(js, ref);

	                }(document));
	            } else {
	                throw ("FB App Id Cannot be blank");
	            }
	        }
	    };

	}]).directive('facebook', ['$http', function ($http) {
	    return {
	        scope: {
	            callback: '=',
	            shares: '='
	        },
	        transclude: true,
	        template: '<div class="facebookButton">' +
				'<div class="pluginButton">' +
				'<div class="pluginButtonContainer">' +
				'<div class="pluginButtonImage">' +
				'<button type="button">' +
				'<i class="pluginButtonIcon img sp_plugin-button-2x sx_plugin-button-2x_favblue"></i>' +
				'</button>' +
				'</div>' +
				'<span class="pluginButtonLabel">Share</span>' +
				'</div>' +
				'</div>' +
				'</div>' +
				'<div class="facebookCount">' +
				'<div class="pluginCountButton pluginCountNum">' +
				'<span ng-transclude></span>' +
				'</div>' +
				'<div class="pluginCountButtonNub"><s></s><i></i></div>' +
				'</div>',
	        link: function (scope, element, attr) {
	            attr.$observe('url', function () {
	                if (attr.shares && attr.url) {
	                    $http.get('https://api.facebook.com/method/links.getStats?urls=' + attr.url + '&format=json').success(function (res) {
	                        var count = res[0] ? res[0].total_count.toString() : 0;
	                        var decimal = '';
	                        if (count.length > 6) {
	                            if (count.slice(-6, -5) != "0") {
	                                decimal = '.' + count.slice(-6, -5);
	                            }
	                            count = count.slice(0, -6);
	                            count = count + decimal + 'M';
	                        } else if (count.length > 3) {
	                            if (count.slice(-3, -2) != "0") {
	                                decimal = '.' + count.slice(-3, -2);
	                            }
	                            count = count.slice(0, -3);
	                            count = count + decimal + 'k';
	                        }
	                        scope.shares = count;
	                    }).error(function () {
	                        scope.shares = 0;
	                    });
	                }
	                element.unbind();
	                element.bind('click', function (e) {
	                    function init(fbId) {
	                        if (fbId) {
	                            this.fbId = fbId;
	                            $window.fbAsyncInit = function () {
	                                FB.init({
	                                    appId: fbId,
	                                    channelUrl: 'app/channel.html',
	                                    status: true,
	                                    xfbml: true
	                                });
	                                FB.ui({
	                                    method: 'share',
	                                    href: attr.url
	                                }, function (response) {
	                                    if (scope.callback !== undefined && typeof scope.callback === "function") {
	                                        scope.callback(response);
	                                    }
	                                });
	                            };
	                            (function (d) {
	                                var js,
                                        id = 'facebook-jssdk',
                                        ref = d.getElementsByTagName('script')[0];
	                                if (d.getElementById(id)) {
	                                    return;
	                                }

	                                js = d.createElement('script');
	                                js.id = id;
	                                js.async = true;
	                                js.src = "//connect.facebook.net/en_US/all.js";

	                                ref.parentNode.insertBefore(js, ref);

	                            }(document));
	                        } else {
	                            throw ("FB App Id Cannot be blank");
	                        }
	                    }
	                    
	                    e.preventDefault();
	                });
	            });
	        }
	    };
	}]).directive('facebookFeedShare', ['$http', function ($http) {
	    return {
	        scope: {
	            callback: '=',
	            shares: '='
	        },
	        transclude: true,
	        template: '<div class="facebookButton">' +
				'<div class="pluginButton">' +
				'<div class="pluginButtonContainer">' +
				'<div class="pluginButtonImage">' +
				'<button type="button">' +
				'<i class="pluginButtonIcon img sp_plugin-button-2x sx_plugin-button-2x_favblue"></i>' +
				'</button>' +
				'</div>' +
				'<span class="pluginButtonLabel">Share</span>' +
				'</div>' +
				'</div>' +
				'</div>' +
				'<div class="facebookCount">' +
				'<div class="pluginCountButton pluginCountNum">' +
				'<span ng-transclude></span>' +
				'</div>' +
				'<div class="pluginCountButtonNub"><s></s><i></i></div>' +
				'</div>',
	        link: function (scope, element, attr) {
	            attr.$observe('url', function () {
	                if (attr.shares && attr.url) {
	                    $http.get('https://api.facebook.com/method/links.getStats?urls=' + attr.url + '&format=json').success(function (res) {
	                        var count = res[0] ? res[0].total_count.toString() : 0;
	                        var decimal = '';
	                        if (count.length > 6) {
	                            if (count.slice(-6, -5) != "0") {
	                                decimal = '.' + count.slice(-6, -5);
	                            }
	                            count = count.slice(0, -6);
	                            count = count + decimal + 'M';
	                        } else if (count.length > 3) {
	                            if (count.slice(-3, -2) != "0") {
	                                decimal = '.' + count.slice(-3, -2);
	                            }
	                            count = count.slice(0, -3);
	                            count = count + decimal + 'k';
	                        }
	                        scope.shares = count;
	                    }).error(function () {
	                        scope.shares = 0;
	                    });
	                }
	                element.unbind();
	                element.bind('click', function (e) {
	                    FB.ui({
	                        method: 'feed',
	                        link: attr.url,
	                        picture: attr.picture,
	                        name: attr.name,
	                        caption: attr.caption,
	                        description: attr.description
	                    }, function (response) {
	                        if (scope.callback !== undefined && typeof scope.callback === "function") {
	                            scope.callback(response);
	                        }
	                    });
	                    e.preventDefault();
	                });
	            });
	        }
	    };
	}]);
//Simple Debounce Implementation
//http://davidwalsh.name/javascript-debounce-function
function debounce(func, wait, immediate) {
    var timeout;
    return function () {
        var context = this,
			args = arguments;
        var later = function () {
            timeout = null;
            if (!immediate) func.apply(context, args);
        };
        var callNow = immediate && !timeout;
        clearTimeout(timeout);
        timeout = setTimeout(later, wait);
        if (callNow) func.apply(context, args);
    };
};
//http://stackoverflow.com/questions/1349404/generate-a-string-of-5-random-characters-in-javascript
/**
 * RANDOM STRING GENERATOR
 *
 * Info:      http://stackoverflow.com/a/27872144/383904
 * Use:       randomString(length [,"A"] [,"N"] );
 * Default:   return a random alpha-numeric string
 * Arguments: If you use the optional "A", "N" flags:
 *            "A" (Alpha flag)   return random a-Z string
 *            "N" (Numeric flag) return random 0-9 string
 */
function randomString(len, an) {
    an = an && an.toLowerCase();
    var str = "",
		i = 0,
		min = an == "a" ? 10 : 0,
		max = an == "n" ? 10 : 62;
    for (; i++ < len;) {
        var r = Math.random() * (max - min) + min << 0;
        str += String.fromCharCode(r += r > 9 ? r < 36 ? 55 : 61 : 48);
    }
    return str;
}