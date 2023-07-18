import React, { useEffect } from 'react';
import logo from './logo.svg';
import {DevTest, sayHello, BeanApi} from "@gary-mcmonagle/coffeeclubapi"
import './App.css';
import { GoogleLogin, GoogleOAuthProvider } from '@react-oauth/google';




export const App = () => {
  const getBean = (accessToken: string) => {
    console.log({accessToken})
    const { getBean } = BeanApi("https://localhost:7231", accessToken);
    getBean()
      .then((res: any) => { console.log({res})})
      .catch((err: any) => { console.log({err})});
  }
  useEffect(() => {
    const { getDt } = DevTest("https://localhost:7231");
    getDt().then((res: any) => {
      console.log({res});
    });

    // const { getBean } = BeanApi("https://localhost:7231", "eyJhbGciOiJSUzI1NiIsImtpZCI6IjY3NmRhOWQzMTJjMzlhNDI5OTMyZjU0M2U2YzFiNmU2NTEyZTQ5ODMiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJhY2NvdW50cy5nb29nbGUuY29tIiwiYXpwIjoiMjg0Nzg0MjMwNzItaGRvY3AyOGM0NmRqajdvdjY5NzZqNW1scHJka3ZrcTkuYXBwcy5nb29nbGV1c2VyY29udGVudC5jb20iLCJhdWQiOiIyODQ3ODQyMzA3Mi1oZG9jcDI4YzQ2ZGpqN292Njk3Nmo1bWxwcmRrdmtxOS5hcHBzLmdvb2dsZXVzZXJjb250ZW50LmNvbSIsInN1YiI6IjExMjM0NTg0MDcwNDg4Njg5OTM0MyIsImVtYWlsIjoiZ2FyeWJtY21vbmFnbGVAZ21haWwuY29tIiwiZW1haWxfdmVyaWZpZWQiOnRydWUsImF0X2hhc2giOiJVRWJocjNkbEtjTktxY28zMjlGV1FnIiwiaWF0IjoxNjg5Njk3Nzk3LCJleHAiOjE2ODk3MDEzOTd9.jsBM8iBvToT-EjNb_vHg9ATyRxq6WdbOEjQFj-rESmDBrjYyTcdbdR_5dR9qNNrFVqbemZ0E0pa_BsN4ec1glufA-3vdV3Qp7ootBudbOwztYdfLSmuz3FDFxQr5vbTtqnxKdNKih8EyJSOYcsP7EEv2f7Grkye8JJv49FAYX-4UFG5wJtlG_czhYmgH0X2Pc5peElpTT3AdPK-nGQLg8ZEKYeqBTOG_VvuSECO1_OVCofvSYMzFUq13RQAqtIr4woJygPozEtfgKIeM9KZfAPnmzyOlYhRqaQDl67e0ot6Ady2jTvPoxQW2B74pJptHmDcmN0IYtMPqMg0_4M2BnQ");
    // getBean()
    //   .then((res: any) => { console.log({res})})
    //   .catch((err: any) => { console.log({err})});

  }, [DevTest]);
  return (
    <GoogleOAuthProvider clientId="28478423072-hdocp28c46djj7ov6976j5mlprdkvkq9.apps.googleusercontent.com">

    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <p>
          {
            sayHello({firstName: "Gary", surname: "McMonagle"})
          }
        </p>
        <GoogleLogin onSuccess={(suc) => {getBean(suc.credential!)}} onError={() => {console.log("err")}} />

      </header>
    </div>
    </GoogleOAuthProvider>
  );
}

export default App;
