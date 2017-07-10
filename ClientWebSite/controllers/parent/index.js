angular.module("module").controller("parent", ["$scope", "getAjax", "$http", "$location", "serverUrl",
    function($scope, getAjax, $http, $location, serverUrl) {
        console.log("Hello From parent");
        $scope.data = "";
        $scope.isLoading = true;

        function getdata() {
            getAjax.response(serverUrl + "api/Parents").
            then((response) => {
                    $scope.isLoading = false;
                    $scope.data = response.data;
                },
                (error) => {
                    console.log(error.statusText)
                });
        }
        getdata();
        $scope.delete = (id) => {
            $scope.deleteId = id;
            $('#pModal').modal('show');
            /*$http.delete(serverUrl + "api/Parents/" + id)
                .then((response) => {
                        console.log(response.statusText);
                        getdata();
                    },
                    (error) => {
                        console.log(error.statusText)
                    });*/
        }
        $scope.confirmDelete = function() {
            $('#pModal').modal("toggle");
            $http.delete(serverUrl + "api/Parents/" + $scope.deleteId)
                .then((response) => {
                        console.log(response.statusText);
                        $scope.data = $scope.data.filter(item => {
                            return item.id != $scope.deleteId;
                        });
                        $scope.deleteId = "";
                    },
                    (error) => {
                        $('#errorModal').modal("toggle");
                        console.log(error.statusText)
                    });
        }
        $scope.update = (d) => {
            $location.path("/updateparent/" + d.id);
        }
        $scope.child = (id) => {
            $location.path("/child/").search({
                Id: id
            });
        }
        $scope.contacts = (id) => {
            $location.path("/contacts/" + id)
            console.log(id)
        }
        $scope.emails = (id) => {
            $location.path("/parentemail/" + id)
            console.log(id)
        }
    }
]);