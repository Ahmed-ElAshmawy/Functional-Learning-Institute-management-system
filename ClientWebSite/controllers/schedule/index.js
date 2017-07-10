angular.module("module").controller("scheduleAll", ["$scope", "getAjax", "$filter", "$http", "$location", "$routeParams", "auth",
    function($scope, getAjax, $filter, $http, $location, $routeParams, auth) {
        getAjax.response("http://localhost:9408/api/Sessions/GetByRecentDate").
        then((data) => {
                var temp = []
                data.data.forEach((i, j) => {
                    if (!temp.includes(i.date)) {
                        temp.push(i.date);
                    }
                });
                var ti = []
                for (var i = 0; i < temp.length; i++) {
                    var item = data.data.filter(function(obj) {
                        return obj.date == temp[i];
                    })
                    console.log(item)
                    ti.push(item);
                    console.log("array bs", ti)
                }
                if (ti.length > 0) {
                    for (i = 1, $scope.maxval = ti[0].length, len = ti.length; i < len; i++) {
                        if ($scope.maxval < ti[i].length) {
                            $scope.maxval = ti[i].length;
                        }
                    }
                    for (i = 0, len = ti.length; i < len; i++) {
                        empty = $scope.maxval - ti[i].length;
                        for (var k = 0; k < empty; k++) {
                            ti[i].push({});
                            console.log("objeeee", ti[i])
                        }
                    }
                }
                console.log("array b3d el ziada", ti)
                $scope.objects = ti;
                $scope.show = data.data;
            },
            (error) => {
                console.log(error);
                if (error.status == "401")
                    $location.path("/unauthorized");
            });
        $scope.showdetails = (id) => {
            $location.path("/sessiondetails/" + id);
        }
    }
]);