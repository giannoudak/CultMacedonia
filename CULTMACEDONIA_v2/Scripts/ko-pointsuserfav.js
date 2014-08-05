

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