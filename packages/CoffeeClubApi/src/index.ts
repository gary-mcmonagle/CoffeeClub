export const Greeter = (name: string) => `Hello ${name}`; 
export const sayHello = ({firstName, surname} : {firstName: string, surname: string}) => `Hello ${firstName} ${surname}`;
import {Configuration, DevTestApi, BeanApi as Bapi} from './generated';

export const DevTest = (basePath: string) => {
    const api = new DevTestApi(new Configuration({basePath}));
    return {
        getDt: async () => api.devTestGet()
    }
}

export const BeanApi = (basePath: string, accessToken: string) => {
    const api = new Bapi(new Configuration({basePath, headers: {Authorization: `Bearer ${accessToken}`}}));
    return {
        getBean: async () => api.beanGet()
    }
};