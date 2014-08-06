

/// <reference path="jquery-2.0.2.js" />
var viewModel = function () {
    $this = this;

    $this.currentPage = ko.observable();
    $this.pageSize = ko.observable(10);
    $this.currentPageIndex = ko.observable(0);
    $this.userfavorites = ko.observableArray();
    $this.sortType = "ascending";
    $this.currentColumn = ko.observable("");
    $this.iconType = ko.observable("");
    $this.currentPage = ko.computed(function () {
        var pagesize = parseInt($this.pageSize(), 10),
        startIndex = pagesize * $this.currentPageIndex(),
        endIndex = startIndex + pagesize;
        return $this.userfavorites.slice(startIndex, endIndex);
    });
    $this.nextPage = function () {
        if ((($this.currentPageIndex() + 1) * $this.pageSize()) < $this.userfavorites().length) {
            $this.currentPageIndex($this.currentPageIndex() + 1);
        }
        else {
            $this.currentPageIndex(0);
        }
    };
    $this.previousPage = function () {
        if ($this.currentPageIndex() > 0) {
            $this.currentPageIndex($this.currentPageIndex() - 1);
        }
        else {
            $this.currentPageIndex((Math.ceil($this.userfavorites().length / $this.pageSize())) - 1);
        }
    };
    $this.isEmpty = function () {
        return $this.userfavorites().length == 0;
    };

    $this.unFavorite = function (favorite) {

        var self = this; // Retain scope of view model

        // do the ajax call to enable the monument
        var _ufplaceId = favorite.PointId;
        var _ufuserId = favorite.UserId;
        var _ufuserName = favorite.UserName;
        var _ufplaceName = favorite.PointName;



        //$.ajax({
        //    url: '/user/favorite_remove',
        //    type: "POST",

        //    data: { pointId: _ufplaceId, username: _ufuserName, userId: _ufuserId, pointName: _ufplaceName }

        //}).done(function (result) {

        //    if (result.success == true) {
        //        self.userfavorites.remove(favorite);
        //    }





        //}).error(function (request, status, err) {
        //    alert('error in enable monument');
        //});


    }.bind(this);

}



$(document).ready(function () {
    
    $.ajax({
        url: "/api/UserFavorites",
        type: "GET",
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
    }).done(function (data) {

        var vm = new viewModel();
        vm.userfavorites(data);
        //var l = vm.contacts().length;
        ko.applyBindings(vm);
    });
});