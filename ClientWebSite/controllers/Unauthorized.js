angular.module("module").controller("unauthorized", ["$scope", "getAjax",
    function($scope, getAjax) {
        console.log("Hello From Unauthorized");
    }
]);