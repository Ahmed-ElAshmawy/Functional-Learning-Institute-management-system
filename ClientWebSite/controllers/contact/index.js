angular.module("module").controller("contacts", ["$scope", "getAjax", "$filter", "$http", "$location", "$routeParams", "serverUrl",
    function($scope, getAjax, $filter, $http, $location, $routeParams, serverUrl) {
        $scope.id = $routeParams.id; //parent id
        $scope.isLoading = true;

        getAjax.response(serverUrl + "api/Contacts/GetByParent?id=" + $scope.id).
        then(function(data) {
            console.log(data.data);
            for (var i = 0; i < data.data.length; i++) {
                switch (data.data[i].contactType) {
                    case 0:
                        data.data[i].contactType = "Emergancy";
                        break;
                    case 1:
                        data.data[i].contactType = "Home";
                        break;
                    case 2:
                        data.data[i].contactType = "Cell Phone";
                        break;
                    case 3:
                        data.data[i].contactType = "Work";
                        break;
                }
            }
            $scope.content = data.data;
            $scope.isLoading = false;
        }, function(error) {
            console.log("error");
        });
        $scope.confirmDelete = () => {
            $('#myModal').modal("toggle");
            $scope.isLoading = true;
            $http.delete(serverUrl + "/api/Contacts/" + $scope.deleteId)
                .then((response) => {
                        console.log(response.statusText);
                        $scope.content = $scope.content.filter(item => {
                            return item.id != $scope.deleteId;
                        });
                        $scope.isLoading = false;
                        $scope.deleteId = "";
                    },
                    (error) => {
                        $('#errorModal').modal("toggle");
                        $scope.isLoading = false;
                        console.log(error.statusText)
                    });

        }
        $scope.delete = (id) => {
            $scope.deleteId = id;
            $('#myModal').modal('show');
        }
        $scope.update = (c) => {
            $location.path("/updateContact/" + c.id);
        }

        $scope.create = () => {

            $location.path("/createContact/" + $scope.id)
        }


    }
]);