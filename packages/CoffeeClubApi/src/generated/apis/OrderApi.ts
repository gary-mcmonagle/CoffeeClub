/* tslint:disable */
/* eslint-disable */
/**
 * CoffeeClub.Core.Api
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
 *
 * The version of the OpenAPI document: v1
 * 
 *
 * NOTE: This class is auto generated by OpenAPI Generator (https://openapi-generator.tech).
 * https://openapi-generator.tech
 * Do not edit the class manually.
 */


import * as runtime from '../runtime';
import type {
  CreateOrderDto,
  OrderCreatedDto,
  OrderDto,
} from '../models';
import {
    CreateOrderDtoFromJSON,
    CreateOrderDtoToJSON,
    OrderCreatedDtoFromJSON,
    OrderCreatedDtoToJSON,
    OrderDtoFromJSON,
    OrderDtoToJSON,
} from '../models';

export interface OrderPostRequest {
    createOrderDto?: CreateOrderDto;
}

/**
 * 
 */
export class OrderApi extends runtime.BaseAPI {

    /**
     */
    async orderGetRaw(initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<Array<OrderDto>>> {
        const queryParameters: any = {};

        const headerParameters: runtime.HTTPHeaders = {};

        const response = await this.request({
            path: `/Order`,
            method: 'GET',
            headers: headerParameters,
            query: queryParameters,
        }, initOverrides);

        return new runtime.JSONApiResponse(response, (jsonValue) => jsonValue.map(OrderDtoFromJSON));
    }

    /**
     */
    async orderGet(initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<Array<OrderDto>> {
        const response = await this.orderGetRaw(initOverrides);
        return await response.value();
    }

    /**
     */
    async orderPostRaw(requestParameters: OrderPostRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<OrderCreatedDto>> {
        const queryParameters: any = {};

        const headerParameters: runtime.HTTPHeaders = {};

        headerParameters['Content-Type'] = 'application/json-patch+json';

        const response = await this.request({
            path: `/Order`,
            method: 'POST',
            headers: headerParameters,
            query: queryParameters,
            body: CreateOrderDtoToJSON(requestParameters.createOrderDto),
        }, initOverrides);

        return new runtime.JSONApiResponse(response, (jsonValue) => OrderCreatedDtoFromJSON(jsonValue));
    }

    /**
     */
    async orderPost(requestParameters: OrderPostRequest = {}, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<OrderCreatedDto> {
        const response = await this.orderPostRaw(requestParameters, initOverrides);
        return await response.value();
    }

}
