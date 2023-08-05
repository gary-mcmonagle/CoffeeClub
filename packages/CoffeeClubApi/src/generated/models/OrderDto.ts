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

import { exists, mapValues } from '../runtime';
import type { DrinkOrderDto } from './DrinkOrderDto';
import {
    DrinkOrderDtoFromJSON,
    DrinkOrderDtoFromJSONTyped,
    DrinkOrderDtoToJSON,
} from './DrinkOrderDto';
import type { OrderStatus } from './OrderStatus';
import {
    OrderStatusFromJSON,
    OrderStatusFromJSONTyped,
    OrderStatusToJSON,
} from './OrderStatus';

/**
 * 
 * @export
 * @interface OrderDto
 */
export interface OrderDto {
    /**
     * 
     * @type {Array<DrinkOrderDto>}
     * @memberof OrderDto
     */
    drinks: Array<DrinkOrderDto>;
    /**
     * 
     * @type {string}
     * @memberof OrderDto
     */
    id: string;
    /**
     * 
     * @type {OrderStatus}
     * @memberof OrderDto
     */
    status: OrderStatus;
}

/**
 * Check if a given object implements the OrderDto interface.
 */
export function instanceOfOrderDto(value: object): boolean {
    let isInstance = true;
    isInstance = isInstance && "drinks" in value;
    isInstance = isInstance && "id" in value;
    isInstance = isInstance && "status" in value;

    return isInstance;
}

export function OrderDtoFromJSON(json: any): OrderDto {
    return OrderDtoFromJSONTyped(json, false);
}

export function OrderDtoFromJSONTyped(json: any, ignoreDiscriminator: boolean): OrderDto {
    if ((json === undefined) || (json === null)) {
        return json;
    }
    return {
        
        'drinks': ((json['drinks'] as Array<any>).map(DrinkOrderDtoFromJSON)),
        'id': json['id'],
        'status': OrderStatusFromJSON(json['status']),
    };
}

export function OrderDtoToJSON(value?: OrderDto | null): any {
    if (value === undefined) {
        return undefined;
    }
    if (value === null) {
        return null;
    }
    return {
        
        'drinks': ((value.drinks as Array<any>).map(DrinkOrderDtoToJSON)),
        'id': value.id,
        'status': OrderStatusToJSON(value.status),
    };
}

