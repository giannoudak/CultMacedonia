

function Category(data) {
    this.name = ko.observable(data.name);
    this.id = ko.observable(data.id);
    this.isInEdit = ko.observable(false);
    //alert(this.isInEdit());

}


// This is simple *viewmodel* - a Javascript tha defines the data and behaviour of UI
var CategoryViewModel = function () {

    var self = this;

    self.categories = ko.observableArray([]);
    self.theTemplate = ko.observable("category-view-tmpl");

    self.displayMode = function (cat) {
        alert(cat.isInEdit());
        return cat.isInEdit() ? "category-edit-tmpl" : "category-view-tmpl";

    };



    self.removeCategory = function (cat) {
        self.categories.remove(cat)
    };


    self.editCategory = function (cat) {
        //alert('go in edit');
        cat.isInEdit(true);
    };


    // Load initial state from server, convert it to Category instances, then populate self.categories
    $.getJSON("/App/GetAllCategories", function (allData) {
        var mappedTasks = $.map(allData, function (item) { return new Category(item) });
        self.categories(mappedTasks);
        //self.categories.push(new Category({ name: "tte", id: 1,isInEdit:false }));
        //self.categories.push(new Category({ name: "tffte", id: 2, isInEdit: false }));
        //self.categories.push(new Category({ name: "tte4fdfdf3", id: 3, isInEdit: false }));

    });


};


// Activate knockout.js
ko.applyBindings(new CategoryViewModel());