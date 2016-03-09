var customDirectives = angular.module('customDirectives', []);

customDirectives.directive('customPopover', function () {
    return {
        restrict: 'A',
        template: '<span>{{t.UsersConfirmed.length}}</span>',
        link: function (scope, el, attrs) {
            $(el).popover({
                trigger: 'click',
                html: true,
                content: "aaaa",
                placement: attrs.popoverPlacement

            })

        }
    }

});