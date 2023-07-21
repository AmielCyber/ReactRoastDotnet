const pageParamType = {
    pageNum: "pageNumber",
    pageSize: "pageSize",
    pageSort: "sort",
    drinkName: "drinkName",
}

const DEFAULT_PAGE_NUM = "1";
const DEFAULT_PAGE_SIZE = "6";

function getURLPageParams(pagePrams: URLSearchParams) {
    const searchParams = new URLSearchParams();

    const pageNumber = pagePrams.get(pageParamType.pageNum) ?? "";
    const pageSize = pagePrams.get(pageParamType.pageSize) ?? "";
    const sort = pagePrams.get(pageParamType.pageSort);
    const drinkName = pagePrams.get(pageParamType.drinkName);

    if (parseInt(pageNumber) > 0) {
        searchParams.append(pageParamType.pageNum, pageNumber);
    } else {
        searchParams.append(pageParamType.pageNum, DEFAULT_PAGE_NUM);
    }

    if (parseInt(pageSize) > 0) {
        searchParams.append(pageParamType.pageSize, pageSize);
    } else {
        searchParams.append(pageParamType.pageSize, DEFAULT_PAGE_SIZE);
    }

    if (sort) {
        searchParams.append(pageParamType.pageSort, sort);
    }
    if (drinkName) {
        searchParams.append(pageParamType.drinkName, drinkName);
    }
    return searchParams;
}

export {getURLPageParams, pageParamType}
