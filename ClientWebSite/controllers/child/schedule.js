angular.module("module").controller("childschedule", ["$scope", "getAjax", "$filter", "$http", "$location", "$routeParams",
    function($scope, getAjax, $filter, $http, $location, $routeParams) {
        $scope.childId = $routeParams.id;
        console.log("child", $scope.childId)
        $("#date").change(() => {
            console.log("changed")
            $scope.date = $("#date").val();
            console.log("date", $scope.date)
            getAjax.response("http://localhost:9408/api/Sessions/GetScheduleSessionsForChild?date=" + $scope.date + "&id=" + $scope.childId)
                .then((data) => {
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
                        }
                        if (ti.length > 0) {

                            for (i = 1, $scope.maxval = ti[0].length, len = ti.length; i < len; i++) {
                                if ($scope.maxval < ti[i].length) {
                                    $scope.maxval = ti[i].length;
                                }
                            }
                            for (i = 1, len = ti.length; i < len; i++) {
                                empty = $.maxval - ti[i].length;
                                for (var k = 0; k < empty; k++) {
                                    ti[i].push({});
                                }
                            }
                        }
                        $scope.objects = ti;
                        $scope.show = data.data;
                    },
                    (error) => {
                        console.log(error);
                        if (error.status == "401")
                            $location.path("/unauthorized");
                    });
        })


        $('#dateDiv').datepicker({
            format: "mm/yyyy",
            startView: 1,
            minViewMode: 1,
            maxViewMode: 2,
            autoclose: true,
            todayHighlight: true,
            toggleActive: true
        });

    }
]);