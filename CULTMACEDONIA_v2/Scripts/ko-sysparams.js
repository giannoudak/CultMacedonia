function formatCurrency(value) {
    return "$" + value.toFixed(2);
}


function Product(id, name, lang) {
    var self = this;
    self.id = id;
    self.name = name;
    self.lang = lang;
}

function ProductViewModel() {
    var self = this;


    self.Product = ko.observable();
    self.Products = ko.observableArray(); // Contains the list of products

    // Initialize the view-model
    $.ajax({
        url: '/App/GetAllCategories',
        cache: false,
        type: 'GET',
        contentType: 'application/json; charset=utf-8',
        data: {},
        success: function (data) {
            self.Products(data); //Put the response in ObservableArray
        }
    });

    self.update = function (Product) {
        alert('going to update');
        alert(Product.name);
    };


    ////Add New Item
    //self.create = function () {
    //    if (Product.Name() != "" && Product.Price() != "" && Product.Category() != "") {
    //        $.ajax({
    //            url: '@Url.Action("AddProduct", "Product")',
    //            cache: false,
    //            type: 'POST',
    //            contentType: 'application/json; charset=utf-8',
    //            data: ko.toJSON(Product),
    //            success: function (data) {
    //                self.Products.push(data);
    //                self.Name("");
    //                self.Price("");
    //                self.Category("");

    //            }
    //        }).fail(
    //        function (xhr, textStatus, err) {
    //            alert(err);
    //        });

    //    }
    //    else {
    //        alert('Please Enter All the Values !!');
    //    }

    //}
    //// Delete product details
    self.delete = function (Product) {
        if (confirm('Are you sure to Delete "' + Product.name + '" product ??')) {
            var id = Product.id;

            //$.ajax({
            //    url: '@Url.Action("AddProduct", "Product")',
            //    cache: false,
            //    type: 'POST',
            //    contentType: 'application/json; charset=utf-8',
            //    data: ko.toJSON(id),
            //    success: function (data) {
            //        self.Products.remove(Product);

            //    }
            //}).fail(
            //function (xhr, textStatus, err) {
            //    self.status(err);
            //});
        }
    }

    //// Edit product details
    //self.edit = function (Product) {
    //    alert('editi me');
    //    //self.Product(Product);

    //};

    //// Update product details
    //self.update = function () {
    //    var Product = self.Product();

    //    $.ajax({
    //        url: '@Url.Action("EditProduct", "Product")',
    //        cache: false,
    //        type: 'PUT',
    //        contentType: 'application/json; charset=utf-8',
    //        data: ko.toJSON(Product),
    //        success: function (data) {
    //            self.Products.removeAll();
    //            self.Products(data); //Put the response in ObservableArray
    //            self.Product(null);
    //            alert("Record Updated Successfully");
    //        }
    //    })
    //    .fail(
    //    function (xhr, textStatus, err) {
    //        alert(err);
    //    });
    //}

    //// Reset product details
    //self.reset = function () {
    //    self.Name("");
    //    self.Price("");
    //    self.Category("");
    //}

    //// Cancel product details
    //self.cancel = function () {
    //    self.Product(null);

    //}
    // Operations
    self.addGift = function () {
        alert('add meeww');
        self.Products.push({
            id:"",
            name: "",
            lang:""
        });
    }

};

var viewModel = new ProductViewModel();
ko.applyBindings(viewModel);