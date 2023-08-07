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
import type { AuthProvider } from './AuthProvider';
import {
    AuthProviderFromJSON,
    AuthProviderFromJSONTyped,
    AuthProviderToJSON,
} from './AuthProvider';

/**
 * 
 * @export
 * @interface User
 */
export interface User {
    /**
     * 
     * @type {AuthProvider}
     * @memberof User
     */
    authProvider?: AuthProvider;
    /**
     * 
     * @type {string}
     * @memberof User
     */
    authId?: string | null;
    /**
     * 
     * @type {string}
     * @memberof User
     */
    id?: string;
}

/**
 * Check if a given object implements the User interface.
 */
export function instanceOfUser(value: object): boolean {
    let isInstance = true;

    return isInstance;
}

export function UserFromJSON(json: any): User {
    return UserFromJSONTyped(json, false);
}

export function UserFromJSONTyped(json: any, ignoreDiscriminator: boolean): User {
    if ((json === undefined) || (json === null)) {
        return json;
    }
    return {
        
        'authProvider': !exists(json, 'authProvider') ? undefined : AuthProviderFromJSON(json['authProvider']),
        'authId': !exists(json, 'authId') ? undefined : json['authId'],
        'id': !exists(json, 'id') ? undefined : json['id'],
    };
}

export function UserToJSON(value?: User | null): any {
    if (value === undefined) {
        return undefined;
    }
    if (value === null) {
        return null;
    }
    return {
        
        'authProvider': AuthProviderToJSON(value.authProvider),
        'authId': value.authId,
        'id': value.id,
    };
}

