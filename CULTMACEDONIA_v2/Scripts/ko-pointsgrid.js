
var _enabled;

/// <reference path="jquery-2.0.2.js" />
var viewModel = function () {
    $this = this;


    // the viewmodel filters man!
    $this.SearchTerm = '', // observable not needed, doesn't trigger any changes
    $this.AddressTerm = '',
    $this.year = '',
    $this.when = '',
    //$this.categoryId = ko.observable();
    //$this.protectionId = ko.observable();
    //$this.propertyId = ko.observable();
    //$this.relegionId = ko.observable();
    //$this.eraId = ko.observable();
    //$this.ethnologicalId = ko.observable();
    $this.petsa='',



    $this.currentPage = ko.observable();
    $this.pageSize = ko.observable(10);
    $this.currentPageIndex = ko.observable(0);
    $this.contacts = ko.observableArray();
    $this.sortType = "ascending";
    $this.currentColumn = ko.observable("");
    $this.iconType = ko.observable("");
    $this.currentPage = ko.computed(function () {
        var pagesize = parseInt($this.pageSize(), 10),
        startIndex = pagesize * $this.currentPageIndex(),
        endIndex = startIndex + pagesize;
        return $this.contacts.slice(startIndex, endIndex);
    });
    $this.nextPage = function () {
        if ((($this.currentPageIndex() + 1) * $this.pageSize()) < $this.contacts().length) {
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
            $this.currentPageIndex((Math.ceil($this.contacts().length / $this.pageSize())) - 1);
        }
    };
    $this.sortTable = function (viewModel, e) {
        var orderProp = $(e.target).attr("data-column")
        $this.currentColumn(orderProp);
        $this.contacts.sort(function (left, right) {
            leftVal = left[orderProp];
            rightVal = right[orderProp];
            if ($this.sortType == "ascending") {
                return leftVal < rightVal ? 1 : -1;
            }
            else {
                return leftVal > rightVal ? 1 : -1;
            }
        });
        $this.sortType = ($this.sortType == "ascending") ? "descending" : "ascending";
        $this.iconType(($this.sortType == "ascending") ? "icon-chevron-up" : "icon-chevron-down");
    };
    $this.GetEquipment = function () {
        var self = this; // Retain scope of view model
        
        $.ajax({
            url: "/api/PointsAdmin",
            type: "GET",
            data: {
                term: self.SearchTerm,
                addr: self.AddressTerm,
                year: self.year,
                active: _enabled,
                when: self.when
                //categoryId: self.categoryId,
                //propertyId: self.propertyId,
                //protectionId: self.protectionId,
                //eraId: self.eraId,
                //ethnolId: self.ethnologicalId,
                //religionId: selft.relegionId
            }
        }).done(function (data) {

            //alert(data.length)
            //var vm = new viewModel();
            //vm.pageSize = ko.observable(10);
            //vm.contacts(data);
            self.currentPageIndex(0);
            self.contacts(data);
            //ko.applyBindings(vm);
        }).error(function () {
            alert('Σφάλμα κατά την αναζήτηση');
        });


    };

    $this.EnableMonument = function (contact) {

        var self = this; // Retain scope of view model

        // do the ajax call to enable the monument
        var _placeId = contact.PointId;
        var _pofuser = contact.UserName;
        var _pName = contact.PlaceName;

        

        $.ajax({
            url: '/Points/EnablePoint',
            type: "POST",
            data: { pointId: _placeId, username: _pofuser, pointName: _pName }

        }).done(function (result) {
            
            if (result.success == true)
            {
                $("#myAdminModal .modal-body p.modal-message").removeClass("alert-danger").addClass("alert alert-success").html(result.message);
                $("#myAdminModal").modal('show');
                $('#myAdminModal').on('hidden.bs.modal', function (event) {

                    self.contacts.remove(contact);

                });
            }
            


           

        }).error(function () {
            alert('error in enable monument');
        });
    }.bind(this);
}

$(document).ready(function () {
    _enabled = $("#hdnActive").val();
    
    $.ajax({
        url: "/api/PointsAdmin",
        type: "GET",
        data: { active: _enabled }
    }).done(function (data) {
        alert(data);
        var vm = new viewModel();
        vm.contacts(data);
        ko.applyBindings(vm);
    });
});