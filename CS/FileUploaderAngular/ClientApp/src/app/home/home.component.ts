import { Component } from '@angular/core';

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
})
export class HomeComponent {
    onUploaded(e) {
        var image = document.createElement("IMG");
        image.setAttribute("src", "data:image/jpg;base64," + e.request.responseText);
        image.classList.add("mr-3");
        var container = document.getElementById("imagesContainer");
        container.appendChild(image);
    }
}
