type Props = {
    pageSize: number;
    currentPage: number;
    totalPages: number;
}

function PageStats(props: Props) {
    return (
        <div className="flex justify-center pb-2">
            <div className="stats shadow max-w-screen-md ">
                <section className="stat place-items-center">
                    <h3 className="stat-title">Page Size</h3>
                    <p className="stat-value">{props.pageSize}</p>
                </section>
                <section className="stat place-items-center">
                    <h3 className="stat-title">Current Page</h3>
                    <p className="stat-value text-secondary">{props.currentPage}</p>
                </section>
                <section className="stat place-items-center">
                    <h3 className="stat-title">Total Pages</h3>
                    <p className="stat-value">{props.totalPages}</p>
                </section>
            </div>
        </div>
    );
}

export default PageStats;
