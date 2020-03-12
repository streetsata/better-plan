const mocks = {
  auth: { POST: { token: 111 } },
  "user/me": { GET: { name: "", title: "" } }
};

const apiCall = ({ url, method }) =>
  new Promise((resolve, reject) => {
    setTimeout(() => {
      try {
        resolve(mocks[url][method || "GET"]);
        console.log(` '${url}' - ${method || "GET"}`);
        console.log(": ", mocks[url][method || "GET"]);
      } catch (err) {
        reject(new Error(err));
      }
    }, 1000);
  });

export default apiCall;
