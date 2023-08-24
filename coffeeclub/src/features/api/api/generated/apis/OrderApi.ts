/* tslint:disable */
/* eslint-disable */
/**
 * OpenAPI Document on Azure Functions
 * This is the OpenAPI Document on Azure Functions
 *
 * The version of the OpenAPI document: 1.0.0
 *
 *
 * NOTE: This class is auto generated by OpenAPI Generator (https://openapi-generator.tech).
 * https://openapi-generator.tech
 * Do not edit the class manually.
 */

import * as runtime from "../runtime";
import type { CreateOrderDto, OrderDto } from "../models";
import {
  CreateOrderDtoFromJSON,
  CreateOrderDtoToJSON,
  OrderDtoFromJSON,
  OrderDtoToJSON,
} from "../models";

export interface AssignOrderRequest {
  orderId: string;
}

export interface CreateOrderRequest {
  body?: CreateOrderDto;
}

/**
 *
 */
export class OrderApi extends runtime.BaseAPI {
  /**
   */
  async assignOrderRaw(
    requestParameters: AssignOrderRequest,
    initOverrides?: RequestInit | runtime.InitOverrideFunction
  ): Promise<runtime.ApiResponse<void>> {
    if (
      requestParameters.orderId === null ||
      requestParameters.orderId === undefined
    ) {
      throw new runtime.RequiredError(
        "orderId",
        "Required parameter requestParameters.orderId was null or undefined when calling assignOrder."
      );
    }

    const queryParameters: any = {};

    const headerParameters: runtime.HTTPHeaders = {};

    const response = await this.request(
      {
        path: `/order/assign/{orderId}`.replace(
          `{${"orderId"}}`,
          encodeURIComponent(String(requestParameters.orderId))
        ),
        method: "POST",
        headers: headerParameters,
        query: queryParameters,
      },
      initOverrides
    );

    return new runtime.VoidApiResponse(response);
  }

  /**
   */
  async assignOrder(
    requestParameters: AssignOrderRequest,
    initOverrides?: RequestInit | runtime.InitOverrideFunction
  ): Promise<void> {
    await this.assignOrderRaw(requestParameters, initOverrides);
  }

  /**
   */
  async createOrderRaw(
    requestParameters: CreateOrderRequest,
    initOverrides?: RequestInit | runtime.InitOverrideFunction
  ): Promise<runtime.ApiResponse<OrderDto>> {
    const queryParameters: any = {};

    const headerParameters: runtime.HTTPHeaders = {};

    headerParameters["Content-Type"] = "application/json";

    const response = await this.request(
      {
        path: `/order`,
        method: "POST",
        headers: headerParameters,
        query: queryParameters,
        body: CreateOrderDtoToJSON(requestParameters.body),
      },
      initOverrides
    );

    return new runtime.JSONApiResponse(response, (jsonValue) =>
      OrderDtoFromJSON(jsonValue)
    );
  }

  /**
   */
  async createOrder(
    requestParameters: CreateOrderRequest = {},
    initOverrides?: RequestInit | runtime.InitOverrideFunction
  ): Promise<OrderDto> {
    const response = await this.createOrderRaw(
      requestParameters,
      initOverrides
    );
    return await response.value();
  }

  /**
   */
  async getAssignableRaw(
    initOverrides?: RequestInit | runtime.InitOverrideFunction
  ): Promise<runtime.ApiResponse<Array<OrderDto>>> {
    const queryParameters: any = {};

    const headerParameters: runtime.HTTPHeaders = {};

    const response = await this.request(
      {
        path: `/order/assignable`,
        method: "GET",
        headers: headerParameters,
        query: queryParameters,
      },
      initOverrides
    );

    return new runtime.JSONApiResponse(response, (jsonValue) =>
      jsonValue.map(OrderDtoFromJSON)
    );
  }

  /**
   */
  async getAssignable(
    initOverrides?: RequestInit | runtime.InitOverrideFunction
  ): Promise<Array<OrderDto>> {
    const response = await this.getAssignableRaw(initOverrides);
    return await response.value();
  }

  /**
   */
  async getOrderRaw(
    initOverrides?: RequestInit | runtime.InitOverrideFunction
  ): Promise<runtime.ApiResponse<Array<OrderDto>>> {
    const queryParameters: any = {};

    const headerParameters: runtime.HTTPHeaders = {};

    const response = await this.request(
      {
        path: `/order`,
        method: "GET",
        headers: headerParameters,
        query: queryParameters,
      },
      initOverrides
    );

    return new runtime.JSONApiResponse(response, (jsonValue) =>
      jsonValue.map(OrderDtoFromJSON)
    );
  }

  /**
   */
  async getOrder(
    initOverrides?: RequestInit | runtime.InitOverrideFunction
  ): Promise<Array<OrderDto>> {
    const response = await this.getOrderRaw(initOverrides);
    return await response.value();
  }
}
