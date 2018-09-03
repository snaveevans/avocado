export const testFetch = (url, method, accessToken) => {
  const myHeaders = new Headers();
  myHeaders.append("Access-Token", accessToken);

  const myInit = {
    method: method,
    headers: myHeaders,
    mode: "cors",
    cache: "default"
  };

  const myRequest = new Request(url);
  fetch(myRequest, myInit)
    .then(r => r.json())
    .then(r => {
        console.log(r);
    })
};
