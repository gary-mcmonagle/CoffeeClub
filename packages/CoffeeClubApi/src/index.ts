export const Greeter = (name: string) => `Hello ${name}`; 
export const sayHello = ({firstName, surname} : {firstName: string, surname: string}) => `Hello ${firstName} ${surname}`;
import {Configuration, DevTestApi} from './generated';

export const DevTest = (basePath: string) => {
    const api = new DevTestApi(new Configuration({basePath}));
    return {
        getDt: async () => api.devTestGet()
    }
}