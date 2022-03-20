import { ResponseCode } from "./response-code";

export class ResponseStatusModel {
    ResponseCode: ResponseCode;
    ResponseMessage: string;
    DataSet: object;
}