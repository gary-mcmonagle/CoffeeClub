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

import { exists, mapValues } from '../runtime';
/**
 * 
 * @export
 * @interface CoffeeBeanMenuDto
 */
export interface CoffeeBeanMenuDto {
    /**
     * 
     * @type {string}
     * @memberof CoffeeBeanMenuDto
     */
    name: string;
    /**
     * 
     * @type {string}
     * @memberof CoffeeBeanMenuDto
     */
    id: string;
}

/**
 * Check if a given object implements the CoffeeBeanMenuDto interface.
 */
export function instanceOfCoffeeBeanMenuDto(value: object): boolean {
    let isInstance = true;
    isInstance = isInstance && "name" in value;
    isInstance = isInstance && "id" in value;

    return isInstance;
}

export function CoffeeBeanMenuDtoFromJSON(json: any): CoffeeBeanMenuDto {
    return CoffeeBeanMenuDtoFromJSONTyped(json, false);
}

export function CoffeeBeanMenuDtoFromJSONTyped(json: any, ignoreDiscriminator: boolean): CoffeeBeanMenuDto {
    if ((json === undefined) || (json === null)) {
        return json;
    }
    return {
        
        'name': json['name'],
        'id': json['id'],
    };
}

export function CoffeeBeanMenuDtoToJSON(value?: CoffeeBeanMenuDto | null): any {
    if (value === undefined) {
        return undefined;
    }
    if (value === null) {
        return null;
    }
    return {
        
        'name': value.name,
        'id': value.id,
    };
}

