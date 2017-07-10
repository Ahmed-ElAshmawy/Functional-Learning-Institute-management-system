angular.module("module").factory("postAjax", ["$http", "auth", function($http, auth) {
    return {
        login: function(url, data) {
            data.grant_type = 'password';
            var req = {
                method: 'POST',
                url: url,
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded'
                },
                data: data,
                transformRequest: function(data, headersGetter) {
                    var str = [];
                    for (var d in data)
                        str.push(encodeURIComponent(d) + "=" +
                            encodeURIComponent(data[d]));
                    return str.join("&");
                }
            }
            return $http(req);
        },
        post: function(url, data) {
            return $http.post(url, data, {
                headers: {
                    'Authorization': auth.getToken(),
                    'content-type': 'application/x-www-form-urlencoded'
                }
            });
        }
    }
}]);