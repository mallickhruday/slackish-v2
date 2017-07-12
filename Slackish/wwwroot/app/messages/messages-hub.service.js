"use strict";
var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
Object.defineProperty(exports, "__esModule", { value: true });
var rxjs_1 = require("rxjs");
var MessagesHubService = (function (_super) {
    __extends(MessagesHubService, _super);
    function MessagesHubService(_zone, _signalRConnectionService) {
        var _this = _super.call(this, null) || this;
        _this._zone = _zone;
        _this._signalRConnectionService = _signalRConnectionService;
        var connection = $.hubConnection("/");
        _this._hub = connection.createHubProxy("messagesHub");
        connection.start().done(function () {
        });
        return _this;
    }
    return MessagesHubService;
}(rxjs_1.BehaviorSubject));
exports.MessagesHubService = MessagesHubService;
//# sourceMappingURL=messages-hub.service.js.map