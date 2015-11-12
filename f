[33mcommit 085890766035a91a62af01bf3498640ab384bb02[m
Author: tbrenneison <tori.brenneison@gmail.com>
Date:   Wed Nov 11 12:53:50 2015 -0500

    Added some details to project detail view

[33mcommit 90ec06da149f41b347c0a874c6ac84b1cd81e431[m
Author: tbrenneison <tori.brenneison@gmail.com>
Date:   Wed Nov 11 12:32:53 2015 -0500

    redid project functionality, when a group is added to a project at project creation the group members are added through a custom join model with personid, projectid, bool removewithgroup

[33mcommit da860fe58c8d039f7c1ec84227549e01b4fb25b9[m
Author: tbrenneison <tori.brenneison@gmail.com>
Date:   Sun Nov 8 17:40:51 2015 -0500

    updated display names for all model properties... do you have to ask about the project task edit action?  no, you don't

[33mcommit 819246dd9827f7fca267324ebe8b8f786e1d3b1c[m
Author: tbrenneison <tori.brenneison@gmail.com>
Date:   Sun Nov 8 17:21:58 2015 -0500

    persons booted from or added to projects based on group add/remove, edit project tasks still broken lol

[33mcommit acfbe12d46463f16597a833ef650ce46a9e227eb[m
Author: tbrenneison <tori.brenneison@gmail.com>
Date:   Sat Nov 7 13:22:41 2015 -0500

    Individuals are assigned to projects now, but the view is wonky because NEEDS MOAR (any) JQUERY

[33mcommit 5c94c18bbd51122687f0eb2e6f4398e958093ff0[m
Author: tbrenneison <tori.brenneison@gmail.com>
Date:   Fri Nov 6 16:58:41 2015 -0500

    Included assigned projects on group detail view.  Edit project task still broken, am ignoring it

[33mcommit 03f433e3e56cc2b229060febd98b166a4305c5a9[m
Author: tbrenneison <tori.brenneison@gmail.com>
Date:   Fri Nov 6 16:49:12 2015 -0500

    cleaning some things up; edit function still borked

[33mcommit 0af88123ecade775675e9dc73b04de9b7d40821d[m
Author: tbrenneison <tori.brenneison@gmail.com>
Date:   Thu Nov 5 20:25:01 2015 -0500

    Making progress toward having project tasks work right

[33mcommit b32e0035bbfb4d51599534f6a81507c32a5af53f[m
Author: tbrenneison <tori.brenneison@gmail.com>
Date:   Thu Nov 5 13:58:52 2015 -0500

    Functionality to add groups and tasks to projects, but it's still ugly, need to arrange for individual project task lists

[33mcommit 14da010b67a7bac1f55d4e2a978f971d70164667[m
Author: tbrenneison <tori.brenneison@gmail.com>
Date:   Mon Nov 2 16:05:59 2015 -0500

    CRUD + views on Projects and ProjectTasks

[33mcommit fae767287679115375194058c39f9f2a56c411fc[m
Author: tbrenneison <tori.brenneison@gmail.com>
Date:   Mon Nov 2 15:24:20 2015 -0500

    changed remove from group dropdown to include only groups the person is already in

[33mcommit 39d8b269ac523e2f185f769e847095c81c207d3d[m
Author: tbrenneison <tori.brenneison@gmail.com>
Date:   Fri Oct 30 13:15:45 2015 -0400

    Added option to view group members from group management index

[33mcommit d8ecc6f6628a8d9add6f5205de191dcc1071435b[m
Author: tbrenneison <tori.brenneison@gmail.com>
Date:   Fri Oct 30 11:48:12 2015 -0400

    Functionality to add and remove members from groups is done, but not nicely

[33mcommit f5f5b42066b5cb1b485a28d2b70c921ea52a718c[m
Author: tbrenneison <tori.brenneison@gmail.com>
Date:   Fri Oct 30 11:24:12 2015 -0400

    Dropdown works, Add To Group functionality WORKS, FINALLY, YES.

[33mcommit ced3bc4c9fb7683f85d534857474d6fdcd30e02e[m
Author: tbrenneison <tori.brenneison@gmail.com>
Date:   Fri Oct 30 11:00:48 2015 -0400

    Workind dropdown and people are in groups but post view not loading correctly... almost there?

[33mcommit c16d58b877c2c79e58c6c2ba9a5c50506f0989c9[m
Author: tbrenneison <tori.brenneison@gmail.com>
Date:   Wed Oct 28 16:33:21 2015 -0400

    now the dropdown works but it will never actually return person.memberships as anything other than null, yay

[33mcommit 46351d41797e9a02579be8a581498fb0aa5c1ed9[m
Author: tbrenneison <tori.brenneison@gmail.com>
Date:   Wed Oct 28 10:03:30 2015 -0400

    Dropdown still doesn't work in AddMemberToGroup, ugh

[33mcommit 838b11077d56a687fae9cb818cfc40ecbbbaaf3f[m
Author: tbrenneison <tori.brenneison@gmail.com>
Date:   Tue Oct 27 13:10:39 2015 -0400

    Initial commit
