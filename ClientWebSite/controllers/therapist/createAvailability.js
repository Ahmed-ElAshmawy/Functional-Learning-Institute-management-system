angular.module("module").controller("createTherapistAvailability", ["$scope", "getAjax", "$filter", "$http", "$location", "$routeParams", "auth",
    function($scope, getAjax, $filter, $http, $location, $routeParams, auth) {
        $scope.id = auth.getUserId();
        $scope.slots = [];
        $scope.slotdatecheck = true;
        $scope.checkedSlots = [];

        $('body').on('click', 'input',
            function() {
                $scope.checkedSlots = [];
                $("[data-checkbox]").each((val, inn) => {
                    if ($(inn).is(":checked")) {
                        var selected = $(inn).attr("data-id");
                        $scope.checkedSlots.push(selected);
                    }
                })
                if ($scope.checkedSlots.length > 0 && !$scope.invalidDate) {
                    $scope.slotdatecheck = false;
                    $scope.$apply()
                } else {
                    $scope.slotdatecheck = true
                    $scope.$apply()
                }
            });

        $('#selectall').click(function() {
            $('input:checkbox').prop('checked', this.checked);
        });
        $("#date").change(() => {
            console.log("changed")
            $scope.date = $("#date").val();
            if (new Date(new Date().toLocaleDateString()) <= new Date($scope.date)) {
                $scope.invalidDate = false;
                $scope.$apply()
                getAjax.response("http://localhost:9408/api/GetSlotsforTherapist?id=" + $scope.id + "&date=" + $scope.date).
                then(function(data) {
                    console.log("data", data.data);
                    for (var i = 0; i < data.data.length; i++) {
                        data.data[i].from = $filter('date')(data.data[i].from, 'HH:mm');
                        data.data[i].to = $filter('date')(data.data[i].to, 'HH:mm');
                    }
                    $scope.content = data.data;
                }, function(error) {
                    console.log("error");
                });

            } else {
                $scope.invalidDate = true
                $scope.$apply()
            }
        })



        $scope.sendData = function() {
            var data = {
                slots: $scope.checkedSlots,
                date: $scope.date,
                therapistId: $scope.id
            };
            console.log(data)
            $http.post("http://localhost:9408/api/TherapistAvailability/Post", data).
            then((data) => {
                    $location.path("/therapistAvailability");
                },
                (error) => {
                    console.log(error);
                });
        };
        $scope.back = () => {
            $location.path("/therapistAvailability");
        }
    }
]);