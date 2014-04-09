
// the Model for every lut in CultMacedonia App
var cultLut = function (name,id,lang) {
    this.name = ko.protectedObservable(name);
    this.id = ko.protectedObservable(id);
    this.lang = ko.protectedObservable(lang);
}


// This is simple *viewmodel* - a Javascript tha defines the data and behaviour of UI
var cultLutViewModel = function () {

    var self = this;

    // our collection
    self.items = ko.observableArray([]);
    this.selectedItem = ko.observable();
    

    // which template to trigger
    self.templateToUse = function (item) {
        //alert(cat.isInEdit());
        //return cat.isInEdit() ? "category-edit-tmpl" : "category-view-tmpl";
        return self.selectedItem() === item ? "category-edit-tmpl" : "category-view-tmpl";
    };


    // onAdding new item handler
    this.addItem = function () {
        var newItem = new Item("new item", 0);
        self.items.push(newItem);
        self.selectedItem(newItem);
    };

    // delete item handler
    this.deleteItem = function (itemToDelete) {
        self.items.remove(itemToDelete);
        self.selectedItem(null);
    };

    // onEdditing item handler
    this.editItem = function (item) {
        self.selectedItem(item);
    };

    // commit changes
    this.acceptItemEdit = function (data) {
        var lutNewValue = $(".editvalue").val();

        var category = {
            id: data.id._latestValue,
            name: lutNewValue,
            lang: data.lang._latestValue
        };

        
        $.ajax({
            type: 'PUT',
            url: "/api/categories/" + +category.id,
            data: JSON.stringify(category),
            contentType: "application/json;charset=utf-8"
        }).done(function (data, textStatus, xhr) {
            if(textStatus =="success"){
                self.selectedItem().name.commit();
                //self.selectedItem().id.commit();
                self.selectedItem(null);
            } else {
                // do sthnig...
            }
            
        }).error(function (jqXHR, textStatus, errorThrown) {
            alert(errorThrown);
        });





        
    };

    // undo-reset changes
    this.cancelItemEdit = function () {
        self.selectedItem().name.reset();
        self.selectedItem().id.reset();
        self.selectedItem(null);
    };


};


//// Activate knockout.js
//ko.applyBindings(new CategoryViewModel());