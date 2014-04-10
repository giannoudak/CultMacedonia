
// the Model for every lut in CultMacedonia App
var cultLut = function (name,id,lang) {
    this.name = ko.protectedObservable(name);
    this.id = ko.protectedObservable(id);
    this.lang = ko.protectedObservable(lang);
    this.isNotNew = ko.computed(function(){
        return (this.id() != -1);
    },this);

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

    // adding item handler
    this.addItem = function () {
       
        var newItem = new cultLut("", -1,"");
        self.items.push(newItem);
        self.selectedItem(newItem);
    };

    // delete item handler
    this.deleteItem = function (itemToDelete) {


        // get elementId
        var id = itemToDelete.id._latestValue;


        $.ajax({
            type: 'DELETE',
            url: "/api/categories/" + +id,
            contentType: "application/json;charset=utf-8"
        }).done(function (data, textStatus, xhr) {
            if (textStatus == "success") {
                self.items.remove(itemToDelete);
                self.selectedItem(null);
            } else {
                // do sthnig...
            }

        }).error(function (jqXHR, textStatus, errorThrown) {
            alert(errorThrown);
        });


        
    };

    // onEdditing item handler
    this.editItem = function (item) {
        self.selectedItem(item);
    };

    // commit changes
    this.acceptItemEdit = function (data) {
        var lutNewValue = $(".editvalue").val();


        // if data.id is -1 we have to POST a new item
        if (data.id() == -1) {

            var category = {
                id: data.id(),
                name: lutNewValue,
                lang: "en"
            };

            $.ajax({
                type: 'POST',
                url: "/api/categories",
                data: JSON.stringify(category),
                contentType: "application/json; charset=utf-8",
            }).done(function (data, textStatus, xhr) {
                if (textStatus == "success") {
                    self.selectedItem().name.commit();
                    //self.selectedItem().id.commit();
                    self.selectedItem(null);
                } else {
                    // do sthnig...
                }

            }).error(function (jqXHR, textStatus, errorThrown) {
                alert(errorThrown);
            });

        } else {

            var category = {
                id: data.id(),
                name: lutNewValue,
                lang: data.lang()
            };


            $.ajax({
                type: 'PUT',
                url: "/api/categories/" + +category.id,
                data: JSON.stringify(category),
                contentType: "application/json;charset=utf-8"
            }).done(function (data, textStatus, xhr) {
                if (textStatus == "success") {
                    self.selectedItem().name.commit();
                    //self.selectedItem().id.commit();
                    self.selectedItem(null);
                } else {
                    // do sthnig...
                }

            }).error(function (jqXHR, textStatus, errorThrown) {
                alert(errorThrown);
            });
        }

        



        
    };

    // undo-reset changes
    this.cancelItemEdit = function () {

        // if cancel clicked and item is temporary (id == -1)
        // we remove it!

        if (self.selectedItem().id() == -1)
        {
            self.items.remove(self.selectedItem());
        }
        
        self.selectedItem().name.reset();
        self.selectedItem().id.reset();
        self.selectedItem(null);
    };


};


