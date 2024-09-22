# The main ideas and architecture

- Content data stores into **yaml** and **markdown** files and all they are source of truth
- You may use the specific component `SxMarkdownContent` to render a **markdown** text from a file or from embedded into
  your **page component**.
- You may use the **yaml page engine** to create predefined types of pages without creating any component.

# The main ideas and motivations by roles:

- I am a developer and I don't want to copy-paste similar pages. Also, I don't want to hard-code content data (or text)
  into page components. I want to store content data in markdown files and to configure them with yaml files. I can use
  Git to control version, as any developer likes. I just may create a page template, configure it, and use it then.
  Also, I do not have to rebuild and republish my site if I just want to quickly edit some text on the site running on
  a hosting.
- I am a site editor and I don't want coding at all. But I have some skills to use Git. My developer creates and
  configures a page template for me. CMS automatically provides me an ability to edit pages using the developed and
  configured templates. Also, the CMS stores the pages as files and I can edit them as files and control my changes
  with the Git.