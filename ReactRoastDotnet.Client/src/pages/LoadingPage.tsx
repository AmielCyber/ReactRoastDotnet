type Props = {
    pageName: string;
}

function LoadingPage(props: Props) {
    return (
        <h1>Loading {props.pageName} page...</h1>
    );
}

export default LoadingPage;
