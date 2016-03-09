var app = angular.module('app', ['ngRoute','ui.bootstrap']).directive('customPopoverconfirmed', function () {
    return {
        restrict: 'A',
        template: '<span>{{t.UsersConfirmed.length}}</span>',
        link: function (scope, el, attrs) {
            $(el).popover({
                trigger: 'click',
                html: true,
                content: scope.getConfirmedUsersHtml(attrs.popoverUsersconfirmed),
                placement: attrs.popoverPlacement

            })

        }
    }
//http://plnkr.co/hYWDccX8fIeWSeg3LA5I
}).directive('datepicker', ['$filter', '$parse', function ($filter, $parse) {
    return {
        restrict: 'EA',
        replace: true,
        scope: {
            model: '=ngModel'
        },
        template: '<table class="well well-large">' +
      '<thead>' +
          '<tr class="text-center">' +
           ' <th><button class="btn pull-left" ng-click="move(-1)"><i class="icon-chevron-left"></i></button></th>' +
            '<th colspan="{{rows[0].length - 2 + showWeekNumber()}}"><button class="btn btn-block" ng-click="toggleMode()"><b>{{title}}</b></button></th>' +
            '<th><button class="btn pull-right" ng-click="move(1)"><i class="icon-chevron-right"></i></button></th>' +
          '</tr>' +
          '<tr class="text-center" ng-show="labels.length > 0">' +
            '<th ng-show="showWeekNumber()">#</th>' +
            '<th ng-repeat="label in labels">{{label}}</th>' +
          '</tr>' +
      '</thead>' +
      '<tbody>' +
          '<tr ng-repeat="row in rows">' +
           '<td ng-show="showWeekNumber()" class="text-center"><i>{{ getWeekNumber(row[0].date) }}</i></td>' +
           '<td ng-repeat="dt in row" class="text-center">' +
               '<button style="width:100%;" class="btn" ng-class="{\'btn-info\': isSelected(dt.date), \'disabled\':  !dt.isCurrent}" ng-click="select(dt.date)" ng-disabled="!dt.selectable">' +
                '{{dt.label}}' +
                '</button></td>' +
          '</tr>' +
      '</tbody>' +
    '</table>',
        link: function (scope, element, attrs) {
            var selected;
            scope.mode = 'day';

            var dayDuration = 86400000; // 1000 * 60 * 60 * 24;

            var startingDay = angular.isDefined(attrs.startingDay) ? scope.$eval(attrs.startingDay) : 0;

            var format = {
                dayLabel: 'EEE',
                day: 'dd',
                month: 'MMMM',
                dayTitle: 'MMMM yyyy',
                monthTitle: 'yyyy'
            };

            var showWeeks = false;
            if (attrs.showWeeks) {
                scope.$parent.$watch($parse(attrs.showWeeks), function (value) {
                    showWeeks = !!value;
                });
            }

            scope.showWeekNumber = function () {
                return (scope.mode === 'day' && showWeeks);
            };

            var split = function (a, size) {
                var arrays = [];
                while (a.length > 0) {
                    arrays.push(a.splice(0, size));
                }
                return arrays;
            };

            var getDaysInMonth = function (year, month) {
                return new Date(year, month + 1, 0).getDate();
            };

            scope.getWeekNumber = function (dt) {
                d = new Date(dt);
                d.setHours(0, 0, 0);
                d.setDate(d.getDate() + 4 - (d.getDay() || 7));
                return Math.ceil((((d - new Date(d.getFullYear(), 0, 1)) / dayDuration) + 1) / 7);
            }

            var fillDays = function () {
                var days = [], labels = [];
                var lastDate = null;

                var addDays = function (dt, n, isCurrentMonth) {
                    for (var i = 0; i < n; i++) {
                        days.push({ date: new Date(dt), isCurrent: isCurrentMonth, selectable: true, label: $filter('date')(dt, format.day) });
                        dt.setDate(dt.getDate() + 1);
                    }
                    lastDate = dt;
                };

                var d = new Date(selected);
                d.setDate(1);

                var difference = startingDay - d.getDay();
                var numDisplayedFromPreviousMonth = (difference > 0) ? 7 - difference : -difference;

                if (numDisplayedFromPreviousMonth > 0) {
                    d.setDate(-numDisplayedFromPreviousMonth + 1);
                    addDays(d, numDisplayedFromPreviousMonth, false);
                }

                addDays(lastDate || d, getDaysInMonth(selected.getFullYear(), selected.getMonth()), true);
                addDays(lastDate, (7 - days.length % 7) % 7, false);

                // Day labels
                for (i = 0; i < 7; i++) {
                    labels.push($filter('date')(days[i].date, format.dayLabel));
                }

                scope.rows = split(days, 7);
                scope.labels = labels;
            };
            var fillMonths = function () {
                var d = new Date(selected), months = [], i = 0;
                while (i < 12) {
                    d.setMonth(i++);
                    months.push({ date: new Date(d), isCurrent: true, selectable: true, label: $filter('date')(d, format.month) });
                }
                scope.rows = split(months, 3);
                scope.labels = [];
            };
            var refill = function () {
                scope.title = $filter('date')(selected, format[scope.mode + 'Title']);

                if (scope.mode === 'day') {
                    fillDays();
                } else if (scope.mode === 'month') {
                    fillMonths();
                }
            };

            scope.$watch('model', function (dt, olddt) {
                if (!angular.isDate(dt)) {
                    throw new Error("Expected `ng-model` to be of type date.");
                }
                selected = angular.copy(dt);

                if (!angular.equals(dt, olddt)) {
                    refill();
                }
            });
            scope.$watch('mode', refill);

            scope.select = function (dt) {
                selected = angular.copy(dt);

                if (scope.mode === 'month') {
                    scope.mode = 'day';
                    selected.setMonth(dt.getMonth());
                } else if (scope.mode === 'day') {
                    scope.model = angular.copy(selected);
                }
            };
            scope.move = function (step) {
                if (scope.mode === 'day') {
                    selected.setMonth(selected.getMonth() + step);
                } else if (scope.mode === 'month') {
                    selected.setFullYear(selected.getFullYear() + step);
                }
                refill();
            };
            scope.isSelected = function (dt) {
                if (angular.isUndefined(dt)) {
                    return false;
                }
                var s = scope.model;
                if (scope.mode === 'day') {
                    return (s.getDate() === dt.getDate() && s.getMonth() === dt.getMonth() && s.getFullYear() === dt.getFullYear());
                } else if (scope.mode === 'month') {
                    return (s.getMonth() === dt.getMonth() && s.getFullYear() === dt.getFullYear());
                }
                return false;
            };
            scope.toggleMode = function () {
                scope.mode = (scope.mode === 'day') ? 'month' : 'day';
            };
        }
    };
}]);


/*
var myapp = angular.module('myapp', ['ng-bootstrap-datepicker']);
angular.injector(['app','myapp']);
*/
app.config(['$routeProvider', function ($routeProvider) {

    $routeProvider.when('/', {
        templateUrl: '/App/Views/Index.html',
        title: 'Index'
    }).when('/Register', {
        templateUrl: '/App/Views/Register.html',
        title: 'Register',
        caseInsentiveMatch: true
    }).when('/Login', {
        templateUrl: '/App/Views/Login.html',
        title: 'Login',
        caseInsentiveMatch: true
    }).when('/Index', {
        templateUrl: '/App/Views/Index.html',
        title: 'Index',
        caseInsentiveMatch: true
    }).when('/UserTrips', {
        templateUrl: '/App/Views/UserTrips.html',
        title: 'My Trips',
        caseInsentiveMatch: true
    }).when('/AllTrips/:Dest', {
        templateUrl: '/App/Views/AllTrips.html',
        title: 'AllTrips',
        caseInsentiveMatch: true
    }).when('/UserProfile/:Username', {
        templateUrl: '/App/Views/UserProfile.html',
        title: 'UserProfile',
    }).when('/AddUserProfile/', {
        templateUrl: '/App/Views/AddUserProfile.html',
        title: 'AddUserProfile',
        caseInsentiveMatch: true
    }).when('/EditProfile/', {
        templateUrl: '/App/Views/EditProfile.html',
        title: 'EditProfile',
        caseInsentiveMatch: true
    }).when('/ViewTrip/', {
        templateUrl: '/App/Views/ViewTrip.html',
        title: 'ViewTrip',
        caseInsentiveMatch: true
    }).otherwise({
        redirectTo: '/'
    });

}]);