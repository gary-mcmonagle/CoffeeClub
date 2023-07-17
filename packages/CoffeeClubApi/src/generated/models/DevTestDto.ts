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
/**
 * 
 * @export
 * @interface DevTestDto
 */
export interface DevTestDto {
    /**
     * 
     * @type {string}
     * @memberof DevTestDto
     */
    name?: string | null;
}

/**
 * Check if a given object implements the DevTestDto interface.
 */
export function instanceOfDevTestDto(value: object): boolean {
    let isInstance = true;

    return isInstance;
}

export function DevTestDtoFromJSON(json: any): DevTestDto {
    return DevTestDtoFromJSONTyped(json, false);
}

export function DevTestDtoFromJSONTyped(json: any, ignoreDiscriminator: boolean): DevTestDto {
    if ((json === undefined) || (json === null)) {
        return json;
    }
    return {
        
        'name': !exists(json, 'name') ? undefined : json['name'],
    };
}

export function DevTestDtoToJSON(value?: DevTestDto | null): any {
    if (value === undefined) {
        return undefined;
    }
    if (value === null) {
        return null;
    }
    return {
        
        'name': value.name,
    };
}

