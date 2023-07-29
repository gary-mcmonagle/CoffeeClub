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
import type { Roast } from './Roast';
import {
    RoastFromJSON,
    RoastFromJSONTyped,
    RoastToJSON,
} from './Roast';
import type { User } from './User';
import {
    UserFromJSON,
    UserFromJSONTyped,
    UserToJSON,
} from './User';

/**
 * 
 * @export
 * @interface CoffeeBean
 */
export interface CoffeeBean {
    /**
     * 
     * @type {string}
     * @memberof CoffeeBean
     */
    id?: string;
    /**
     * 
     * @type {string}
     * @memberof CoffeeBean
     */
    name?: string | null;
    /**
     * 
     * @type {Roast}
     * @memberof CoffeeBean
     */
    roast?: Roast;
    /**
     * 
     * @type {string}
     * @memberof CoffeeBean
     */
    description?: string | null;
    /**
     * 
     * @type {boolean}
     * @memberof CoffeeBean
     */
    inStock?: boolean;
    /**
     * 
     * @type {User}
     * @memberof CoffeeBean
     */
    createdBy?: User;
}

/**
 * Check if a given object implements the CoffeeBean interface.
 */
export function instanceOfCoffeeBean(value: object): boolean {
    let isInstance = true;

    return isInstance;
}

export function CoffeeBeanFromJSON(json: any): CoffeeBean {
    return CoffeeBeanFromJSONTyped(json, false);
}

export function CoffeeBeanFromJSONTyped(json: any, ignoreDiscriminator: boolean): CoffeeBean {
    if ((json === undefined) || (json === null)) {
        return json;
    }
    return {
        
        'id': !exists(json, 'id') ? undefined : json['id'],
        'name': !exists(json, 'name') ? undefined : json['name'],
        'roast': !exists(json, 'roast') ? undefined : RoastFromJSON(json['roast']),
        'description': !exists(json, 'description') ? undefined : json['description'],
        'inStock': !exists(json, 'inStock') ? undefined : json['inStock'],
        'createdBy': !exists(json, 'createdBy') ? undefined : UserFromJSON(json['createdBy']),
    };
}

export function CoffeeBeanToJSON(value?: CoffeeBean | null): any {
    if (value === undefined) {
        return undefined;
    }
    if (value === null) {
        return null;
    }
    return {
        
        'id': value.id,
        'name': value.name,
        'roast': RoastToJSON(value.roast),
        'description': value.description,
        'inStock': value.inStock,
        'createdBy': UserToJSON(value.createdBy),
    };
}
