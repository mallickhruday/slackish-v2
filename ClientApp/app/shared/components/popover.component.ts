import { Component, Input, OnInit, ViewEncapsulation, ElementRef } from "@angular/core";
import { LogOutClicked, popoverEvents } from "../events";

@Component({
    templateUrl: "./popover.component.html",
    styleUrls: ["./popover.component.css"],
    selector: "ce-popover",
    encapsulation: ViewEncapsulation.Emulated
})
export class PopoverComponent {

    constructor(private _elementRef: ElementRef) {}

    public logoutClicked() {
        this._elementRef.nativeElement.dispatchEvent(new CustomEvent(popoverEvents.USERNAME_CLICK, {
            bubbles: true,
            cancelable: true,
            composed: true
        } as CustomEventInit));        
    }
}
