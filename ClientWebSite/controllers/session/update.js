angular.module("module").controller("updateSession", ["$scope", "getAjax", "$http", "$location", "serverUrl", "$routeParams",
    function($scope, getAjax, $http, $location, serverUrl, $routeParams) {
        $scope.id = $routeParams.Id;
        //if true show old terapiest
        $scope.type = $routeParams.type;
        console.log($scope.id)
        console.log($scope.type)
        $scope.therapists = []
        $scope.isLoading = true;
        getAjax.get(serverUrl + "api/Sessions/GetUpdateSession/" + $scope.id).
        then(function(data) {
            $scope.show = data.data
            $scope.isLoading = false;
            $scope.session = data.data.session;
            console.log(data.data);
            // $("#sessionStatus").val(data.data.session.status);
            $scope.therapists = data.data.therapists;
            //show old  therapist
            if ($scope.type) {
                $scope.therapists.push({
                    id: data.data.session.therapistId,
                    value: data.data.session.therapistName
                });
                $scope.therapistId = data.data.session.therapistId;

            } else {
                if ($scope.therapists.length > 0)
                    $scope.therapistId = $scope.therapists[0].id;
            }

            data.data.session.children.forEach(obj => obj.active = true);
            $scope.checkedChildren = data.data.session.children.map(function(a) { return a.id; });

            data.data.children.forEach(obj => obj.active = false);
            $scope.children = data.data.children.concat(data.data.session.children)
        }, function(error) {
            if (error.status == "401")
                $location.path("/unauthorized");
            console.log(error);
        });
        $('body').on('click', 'input',
            function() {
                $scope.checkedChildren = [];
                $("[data-checkbox]").each((val, inn) => {
                    if ($(inn).is(":checked")) {
                        var selected = $(inn).attr("value");
                        $scope.checkedChildren.push(selected);
                        $scope.$apply()
                    }
                })
            });


        $scope.update = () => {
            console.log($scope.type);

            //$scope.session 
            var obj = {
                date: $scope.session.date,
                id: $scope.session.id,
                status: $scope.session.status,
                therapistId: $scope.therapistId,
                children: $scope.checkedChildren.map(function(a) { return { id: a }; })
            };
            console.log(obj);
            if ($scope.type) {
                //update & make Therapist free
                getAjax.put(serverUrl + "api/sessions/PutSession?id=" + $scope.id + "&status=true", obj).
                then((response) => {
                        console.log(response);
                        $location.path("/session");

                    },
                    (error) => {
                        console.log(error);
                    });
            } else {
                //update only
                getAjax.put(serverUrl + "api/sessions/PutSession?id=" + $scope.id + "&status=false", obj).
                then((response) => {
                        console.log(response);
                        $location.path("/session");

                    },
                    (error) => {
                        console.log(error);
                    });
            }
        }
        $scope.back = function() {
            $location.path("/session/");
        }

        $scope.delete = () => {
            console.log($scope.type);
            if ($scope.type) {
                //cancel & make all free
                getAjax.put(serverUrl + "api/sessions/CancelSession?id=" + $scope.id + "&status=true", {}).
                then((response) => {
                        console.log(response);
                        $location.path("/session");

                    },
                    (error) => {
                        console.log(error);
                    });
            } else {
                //cancel & make children only free
                getAjax.put(serverUrl + "api/sessions/CancelSession?id=" + $scope.id + "&status=false", {}).
                then((response) => {
                        console.log(response);
                        $location.path("/session");

                    },
                    (error) => {
                        console.log(error);
                    });
            }
        }
    }
]);